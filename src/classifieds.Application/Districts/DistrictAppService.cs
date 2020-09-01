using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using classifieds.Authorization;
using classifieds.Districts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace classifieds.Districts
{
    public class DistrictAppService : AsyncCrudAppService<District, DistrictDto>, IDistrictAppService
    {
        private readonly IRepository<District> _districtSrvice;
        private readonly IObjectMapper _objectMapper;
        public DistrictAppService(IRepository<District> districtSrvice, IObjectMapper objectMapper) : base(districtSrvice)
        {
            _districtSrvice = districtSrvice;
            _objectMapper = objectMapper;

            CreatePermissionName = PermissionNames.Pages_District;
            UpdatePermissionName = PermissionNames.Pages_District;
            DeletePermissionName = PermissionNames.Pages_District;
        }
        public async Task<List<DistrictDto>> GetByCityId(int id)
        {
            var districts = await _districtSrvice.GetAllListAsync(m => m.CityId == id);
            return _objectMapper.Map<List<DistrictDto>>(districts);
        }
    }
}
