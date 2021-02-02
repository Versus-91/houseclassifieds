using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Areas;
using classifieds.Areas.Dto;
using classifieds.Authorization;
using classifieds.Cities;
using classifieds.Cities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Areas)]
    public class AreasController : AbpController
    {
        private readonly IAreaAppService _areaService;
        private readonly ICityAppService _cityService;

        public AreasController(IAreaAppService areaService, ICityAppService cityService)
        {
            _areaService = areaService;
            _cityService = cityService;
        }

        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var cities = await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = Int32.MaxValue });
            return View(cities.Items);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _areaService.GetAsync(new EntityDto(id));
            var cities = await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto() { MaxResultCount = Int32.MaxValue });
            ViewData["cities"] = new SelectList(cities.Items, nameof(CityDto.Id), nameof(CityDto.Name));
            return PartialView("_EditModal", model);
        }
    }
}
