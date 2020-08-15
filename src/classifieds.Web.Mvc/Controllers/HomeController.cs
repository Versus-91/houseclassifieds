using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using classifieds.Controllers;
using classifieds.Cities;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using classifieds.PropertyTypes;
using classifieds.Categories;
using classifieds.Web.Models.Ads;
using System.Linq;
using classifieds.Cities.Dto;
using classifieds.Categories.Dto;
using classifieds.PropertyTypes.Dto;

namespace classifieds.Web.Controllers
{
    public class HomeController : classifiedsControllerBase
    {
        private readonly ICityAppService _cityService;
        private readonly ITypeAppService _typeService;
        private readonly ICategoryAppService _categoryService;

        public HomeController(ICityAppService cityService, ICategoryAppService categoryService, ITypeAppService typeService)
        {
            _cityService = cityService;
            _categoryService = categoryService;
            _typeService = typeService;
        }
        public async Task<ActionResult> Index()
        {
            var cities = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = int.MaxValue })).Items.ToList();
            var categories = (await _categoryService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = int.MaxValue })).Items;
            var types = (await _typeService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = int.MaxValue })).Items.ToList();
            cities.Insert(0, new CityDto { Id = 0, Name = "شهر را انتخاب کنید" });
            types.Insert(0, new PropertyTypeDto { Id = 0, Name = "نوع ملک را انتخاب کنید" });
            var model = new AdsIndexViewModel
            {
                Cities = cities,
                Categories = categories,
                Types = types

            };
            return View(model);
        }
        public ActionResult Admin()
        {
            return View();
        }
    }
}
