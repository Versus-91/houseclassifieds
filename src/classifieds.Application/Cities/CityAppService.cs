using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.Cities.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace classifieds.Cities
{
    public class CityAppService : AsyncCrudAppService<City, CityDto>, ICityAppService
    {
        public CityAppService(IRepository<City> repository) : base(repository)
        {
            CreatePermissionName = PermissionNames.Pages_Cities;
            UpdatePermissionName = PermissionNames.Pages_Cities;
            DeletePermissionName = PermissionNames.Pages_Cities;
        }

    }
}
