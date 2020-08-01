using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using classifieds.Controllers;
using classifieds.Cities;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;

namespace classifieds.Web.Controllers
{
    public class HomeController : classifiedsControllerBase
    {
        private readonly ICitiesAppService _cityService;
        public HomeController(ICitiesAppService cityService)
        {
            _cityService = cityService;
        }
        public async Task<ActionResult> Index()
        {
            ViewData["cities"] = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items;

            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
    }
}
