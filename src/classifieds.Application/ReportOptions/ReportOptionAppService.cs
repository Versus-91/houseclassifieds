using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.ReportOptions.Dto;

namespace classifieds.ReportOptions
{
    public class ReportOptionAppService : AsyncCrudAppService<ReportOption, ReportOptionDto, int>
    {
        public ReportOptionAppService(IRepository<ReportOption> repository) :base(repository)
        {
            UpdatePermissionName = PermissionNames.Pages_AdminPosts;
            DeletePermissionName = PermissionNames.Pages_AdminPosts;
            CreatePermissionName = PermissionNames.Pages_AdminPosts;
        }
    }
 
}
