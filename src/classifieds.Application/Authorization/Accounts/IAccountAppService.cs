using System.Threading.Tasks;
using Abp.Application.Services;
using classifieds.Authorization.Accounts.Dto;
using classifieds.Users.Dto;

namespace classifieds.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
        UserDto User();
    }
}
