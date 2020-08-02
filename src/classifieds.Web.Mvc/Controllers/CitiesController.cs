using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Cities;
using classifieds.Cities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace classifieds.Web.Controllers
{
    public class CitiesController : AbpController
    {
        private readonly ICityAppService _cityService;
        public CitiesController(ICityAppService cityService)
        {
            _cityService = cityService;
        }
        // GET: CategoriesController
        public  ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _cityService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", model);
        }
    }
}
