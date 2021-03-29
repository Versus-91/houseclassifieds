using Abp.Application.Services.Dto;
using classifieds.Cities;
using classifieds.Cities.Dto;
using classifieds.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Controllers
{
    public class AgenciesController : classifiedsControllerBase
    {
        private readonly ICityAppService _cityService;
        public AgenciesController(ICityAppService cityService)
        {
            _cityService = cityService;
        }
        public async Task<IActionResult> Index()
        {
            var cities = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = int.MaxValue })).Items.ToList();
            cities.Insert(0, new CityDto { Id = 0, Name = "شهر را انتخاب کنید" });
            ViewData["cities"] = cities;
            return View();
        }
    }
}
