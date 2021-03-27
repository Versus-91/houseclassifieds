using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Cities;
using classifieds.Cities.Dto;
using classifieds.RealEstates;
using classifieds.RealEstates.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RealEstatesController : AbpController
    {
        private readonly ICityAppService _cityService;
        private readonly RealestateAppService _realestateService;
        private readonly IWebHostEnvironment _env;
        private readonly string _path;
        public RealEstatesController(ICityAppService cityService, RealestateAppService realestateService, IWebHostEnvironment env,
            IConfiguration config)
        {
            _path = config.GetValue<string>("IconsFilesPath");
            _env = env;
            _cityService = cityService;
            _realestateService = realestateService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var cities = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = int.MaxValue })).Items.ToList();
            cities.Insert(0, new CityDto { Id = 0, Name = "شهر را انتخاب کنید" });
            ViewData["Cities"] = new SelectList(cities, nameof(CityDto.Id), nameof(CityDto.Name));

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RealEstateDto inputs)
        {

            if (ModelState.IsValid)
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

                    inputs.Logo = Path.Combine(_path, trustedFileNameForFileStorage);
                }
                await _realestateService.CreateAsync(inputs);
                return Ok();
            }
            return View();
        }
    }
}
