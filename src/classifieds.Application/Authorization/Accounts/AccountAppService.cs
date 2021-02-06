using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Abp.UI;
using Abp.Zero.Configuration;
using classifieds.Authorization.Accounts.Dto;
using classifieds.Authorization.Users;
using classifieds.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AccountUserDto = classifieds.Authorization.Accounts.Dto.AccountUserDto;

namespace classifieds.Authorization.Accounts
{
    public class AccountAppService : classifiedsAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly UserManager _userManager;
        private readonly UserStore _userStore;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager, UserManager userManager, UserStore userStore, IAbpSession abpSession, LogInManager logInManager, IPasswordHasher<User> passwordHasher)
        {
            _userRegistrationManager = userRegistrationManager;
            _userManager = userManager;
            _userStore = userStore;
            _abpSession = abpSession;
            _logInManager = logInManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                false, // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
                input.PhoneNumber
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
        [HttpGet]
        [AbpAuthorize]
        public AccountUserDto User()
        {
            var user =  _userManager.GetUserById(AbpSession.UserId.Value);
    
            return ObjectMapper.Map<AccountUserDto>(user);
        }
        [HttpPut]
        public async Task<bool> ChangePassword(ChangePasswordInput input)
        {
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.OldPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
         
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;

        }

        protected  void MapToEntity(AccountUserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }
        [HttpPost]
        public async Task<string> CheckUsername(CheckUserInput input)
        {
          
                var user = await _userManager.FindByNameAsync(input.username);
                if (user == null)
                {
                    return  "available";
                }
                return "unavailable";
            
        }
        [HttpPost]
        public async Task<string> CheckPhoneNumber(CheckPhoneInput input)
        {

            var user = await _userStore.UserRepository.FirstOrDefaultAsync(m=>m.PhoneNumber.Equals(input.PhoneNumber));
            if (user == null)
            {
                return "available";
            }
            return "unavailable";

        }
        [HttpPost]
        public async Task<string> CheckEmail(CheckEmailInput input)
        {

            var user = await _userManager.FindByEmailAsync(input.EmailAddress);
            if (user == null)
            {
                return "available";
            }
            return "unavailable";

        }
        [HttpPut]
        public async Task<AccountUserDto> UpdateAsync(AccountUserDto input)
        {

            var user = await _userManager.GetUserByIdAsync(input.Id);
            if (user.Id != AbpSession.UserId)
            {
                throw new UserFriendlyException("You cannot change this user info.");
            }

            user.Name = input.Name;
            user.Surname = input.Surname;
            user.EmailAddress = input.EmailAddress;
            user.SetNormalizedNames();
            CheckErrors(await _userManager.UpdateAsync(user));

            return ObjectMapper.Map<AccountUserDto>(await _userManager.GetUserByIdAsync(input.Id)) ;
        }
    }
}
