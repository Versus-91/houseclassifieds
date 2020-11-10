using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using classifieds.Authorization;
using classifieds.Cities;
using classifieds.Districts.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Districts
{
    public class DistrictAppService : AsyncCrudAppService<District, DistrictDto>, IDistrictAppService
    {
        private readonly IRepository<District> _districtService;
        private readonly IRepository<City> _cityService;
        private readonly IObjectMapper _objectMapper;
        public DistrictAppService(IRepository<District> districtSrvice, IRepository<City> cityService, IObjectMapper objectMapper) : base(districtSrvice)
        {
            _districtService = districtSrvice;
            _objectMapper = objectMapper;
            _cityService = cityService;
            CreatePermissionName = PermissionNames.Pages_District;
            UpdatePermissionName = PermissionNames.Pages_District;
            DeletePermissionName = PermissionNames.Pages_District;
        }
        public async Task<List<DistrictDto>> GetByCityId(int id)
        {
            var districts = await _districtService.GetAllListAsync(m => m.CityId == id);
            return _objectMapper.Map<List<DistrictDto>>(districts);
        }
        public async Task<List<LocationSearchDto>> Find(string query)
        {
            var districts = await _districtService.GetAllListAsync(m=>m.Name.Contains(query));
            var cities = (await _cityService.GetAllListAsync(m => m.Name.Contains(query))).Select(m => new LocationSearchDto()
            {
                Id = m.Id,
                Name = m.Name,
                IsCity = true
            }).ToList();
            var searchResult = districts.Select(m=> new LocationSearchDto() { 
                Id = m.Id,
                Name = m.Name,
                IsCity= false
            }).ToList();
            searchResult.AddRange(cities);
            return searchResult;
        }
    }
}
