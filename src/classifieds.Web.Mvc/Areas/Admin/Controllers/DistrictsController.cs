using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Web.Models;
using classifieds.Areas;
using classifieds.Authorization;
using classifieds.Cities;
using classifieds.Cities.Dto;
using classifieds.Districts;
using classifieds.Districts.Dto;
using classifieds.Web.Models.Districts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DistrictsController : AbpController
    {
        private readonly IDistrictAppService _districtService;
        private readonly ICityAppService _cityService;
        private readonly IAreaAppService _areaService;


        public DistrictsController(IDistrictAppService districtService, ICityAppService cityService, IAreaAppService areaService)
        {
            _districtService = districtService;
            _cityService = cityService;
            _areaService = areaService;
        }
        [AbpMvcAuthorize(PermissionNames.Pages_District)]

        public async Task<ActionResult> Index()
        {
            var cities = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = Int32.MaxValue })).Items.ToList();
            var model = new DistrictViewModel
            {
                Cities =cities
            };
            return View(model);
        }
        [AbpMvcAuthorize(PermissionNames.Pages_District)]

        public async Task<ActionResult> Edit(int id)
        {
            var district = await _districtService.GetAsync(new EntityDto(id));
            var cities = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = Int32.MaxValue })).Items.ToList();
            var areas = (await _areaService.GetAllAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = Int32.MaxValue })).Items.ToList();
            var model = new DistrictViewModel
            {
                District = district,
                Cities = cities,
                Areas = areas,
            };
            return PartialView("_EditModal", model);
        }
        public async Task<AjaxResponse> GetByCityId(int id)
        {
            var districts = await _districtService.GetByCityId(id);
            return new AjaxResponse(districts);
        }
    }
}