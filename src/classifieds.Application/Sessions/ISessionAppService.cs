using System.Threading.Tasks;
using Abp.Application.Services;
using classifieds.Sessions.Dto;

namespace classifieds.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
