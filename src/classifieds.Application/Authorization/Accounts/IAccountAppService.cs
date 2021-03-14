using System.Threading.Tasks;
using Abp.Application.Services;
using classifieds.Authorization.Accounts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace classifieds.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
        AccountUserDto User();
        Task<AccountUserDto> UpdateAsync(AccountUserDto input);
        Task<bool> ChangePassword(ChangePasswordInput input);
        Task<string> UserPhoneNumber(long userId);
    }
}
