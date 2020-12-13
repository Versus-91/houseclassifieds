using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Amenities;
using classifieds.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Amenities)]
    public class AmenitiesController : AbpController
    {
        private readonly AmenityAppService _amenityService;
        public AmenitiesController(AmenityAppService amenityService)
        {
            _amenityService = amenityService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _amenityService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", model);
        }
    }
}
