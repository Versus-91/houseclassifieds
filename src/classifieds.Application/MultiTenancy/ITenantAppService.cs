using Abp.Application.Services;
using classifieds.MultiTenancy.Dto;

namespace classifieds.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

