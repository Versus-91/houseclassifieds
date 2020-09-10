using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.PropertyTypes.Dto;

namespace classifieds.PropertyTypes
{
    public class TypeAppService: AsyncCrudAppService<PropertyType,PropertyTypeDto>, ITypeAppService
    {
        public TypeAppService(IRepository<PropertyType> repository):base(repository)
        {
            CreatePermissionName = PermissionNames.Pages_PropertyTypes;
            UpdatePermissionName = PermissionNames.Pages_PropertyTypes;
            DeletePermissionName = PermissionNames.Pages_PropertyTypes;
        }
    }
}
