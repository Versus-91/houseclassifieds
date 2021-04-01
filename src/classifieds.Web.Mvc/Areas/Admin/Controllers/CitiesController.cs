using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Authorization;
using classifieds.Cities;
using classifieds.Cities.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Cities)]
    public class CitiesController : AbpController
    {
        private readonly ICityAppService _cityService;

        public CitiesController(ICityAppService cityService)
        {
            _cityService = cityService;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CityDto inputs)
        {

            if (ModelState.IsValid)
            {
                await _cityService.CreateAsync(inputs);
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _cityService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", model);
        }

    }
}