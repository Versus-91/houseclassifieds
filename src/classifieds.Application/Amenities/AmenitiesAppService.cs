using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Amenities.Dto;
using classifieds.Authorization;

namespace classifieds.Amenities
{
    public class AmenityAppService: AsyncCrudAppService<Amenity, AmenityDto>
    {
        public AmenityAppService(IRepository<Amenity> repository) :base(repository: repository)
        {
            CreatePermissionName = PermissionNames.Pages_Amenities;
            UpdatePermissionName = PermissionNames.Pages_Amenities;
            DeletePermissionName = PermissionNames.Pages_Amenities;
        }
    }
}
