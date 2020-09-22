using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using classifieds.Amenities.Dto;
using classifieds.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Amenities
{
    [AbpAuthorize(PermissionNames.Pages_Amenities)]
    public class AmenityAppService: AsyncCrudAppService<Amenity, AmenityDto>
    {
        public AmenityAppService(IRepository<Amenity> repository) :base(repository: repository)
        {

        }
    }
}
