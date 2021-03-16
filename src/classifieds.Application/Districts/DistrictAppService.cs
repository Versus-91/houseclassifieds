using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using classifieds.Areas;
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
        private readonly IRepository<Area> _areaService;

        private readonly IObjectMapper _objectMapper;
        public DistrictAppService(IRepository<Area> areaService,IRepository<District> districtSrvice, IRepository<City> cityService, IObjectMapper objectMapper) : base(districtSrvice)
        {
            _areaService = areaService;
            _districtService = districtSrvice;
            _objectMapper = objectMapper;
            _cityService = cityService;
            CreatePermissionName = PermissionNames.Pages_District;
            UpdatePermissionName = PermissionNames.Pages_District;
            DeletePermissionName = PermissionNames.Pages_District;
        }
        protected override IQueryable<District> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return base.CreateFilteredQuery(input)
                .Include(m => m.Area)
                .ThenInclude(m => m.City)
                .OrderByDescending(m => m.CreationTime);
        }
        public async Task<List<DistrictDto>> GetByCityId(int id)
        {
            var districts = await _districtService.GetAllListAsync(m => m.Area.CityId == id);
            return _objectMapper.Map<List<DistrictDto>>(districts);
        }
        public async Task<DistrictDto> GetById(int id)
        {
            var districts = await _districtService.GetAllIncluding(m => m.Area).Where(m=>m.Id == id).FirstOrDefaultAsync();
            return _objectMapper.Map<DistrictDto>(districts);
        }
        public async Task<List<DistrictDto>> GetByAreaId(int id)
        {
            var districts = await _districtService.GetAllListAsync(m => m.AreaId == id);
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
