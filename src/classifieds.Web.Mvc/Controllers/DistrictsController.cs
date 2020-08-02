using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Cities;
using classifieds.Cities.Dto;
using classifieds.Districts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace classifieds.Web.Controllers
{
    public class DistrictsController : AbpController
    {
        private readonly IDistrictAppService _districtService;
        public DistrictsController(IDistrictAppService districtService)
        {
            _districtService = districtService;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _districtService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", model);
        }
    }
}
