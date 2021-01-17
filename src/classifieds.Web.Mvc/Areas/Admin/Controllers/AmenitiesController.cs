using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Amenities;
using classifieds.Amenities.Dto;
using classifieds.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Amenities)]
    public class AmenitiesController : AbpController
    {
        private readonly AmenityAppService _amenityService;
        private readonly IWebHostEnvironment _env;
        private readonly string _path;

        public AmenitiesController(AmenityAppService amenityService, IConfiguration config, IWebHostEnvironment env)
        {
            _path = config.GetValue<string>("IconsFilesPath");
            _env = env;
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
        [HttpPost]
        public async Task<ActionResult> Create(AmenityDto inputs)
        {
            if (inputs.File != null)
            {
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                    inputs.File.FileName);
                var trustedFileNameForFileStorage = Path.Combine($"{Guid.NewGuid().ToString("N")}{Path.GetExtension(trustedFileNameForDisplay).ToLower()}");
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _path));
                using (var stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, _path, trustedFileNameForFileStorage)))
                {
                    await inputs.File.CopyToAsync(stream);
                }

                inputs.Icon = Path.Combine(_path, trustedFileNameForFileStorage);
            }
            var model = await _amenityService.CreateAsync(inputs);
            return Redirect("/admin/amenities");
        }
    }
}

