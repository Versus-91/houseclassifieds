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
            await _amenityService.CreateAsync(inputs);
            return Redirect("/admin/amenities");
        }
        [HttpPost]
        public async Task<ActionResult> Update(AmenityDto inputs)
        {
            var amenity = await _amenityService.GetAsync(new EntityDto(inputs.Id));
            if (amenity == null)
            {
                return NotFound();
            }
            amenity = ObjectMapper.Map( inputs, amenity);
            if (inputs.File != null)
            {
                if (!String.IsNullOrEmpty(amenity.Icon))
                {
                    System.IO.File.Delete(Path.Combine(_env.WebRootPath, amenity.Icon));
                }
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                    inputs.File.FileName);
                var trustedFileNameForFileStorage = Path.Combine($"{Guid.NewGuid().ToString("N")}{Path.GetExtension(trustedFileNameForDisplay).ToLower()}");
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _path));
                using (var stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, _path, trustedFileNameForFileStorage)))
                {
                    await inputs.File.CopyToAsync(stream);
                }

                amenity.Icon = Path.Combine(_path, trustedFileNameForFileStorage);
            }

            await _amenityService.UpdateAsync(amenity);
            return Redirect("/admin/amenities");
        }
    }
}

