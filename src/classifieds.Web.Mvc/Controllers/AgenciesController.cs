using Abp.Application.Services.Dto;
using classifieds.Cities;
using classifieds.Cities.Dto;
using classifieds.Controllers;
using classifieds.Posts;
using classifieds.Posts.Dto;
using classifieds.RealEstates;
using classifieds.Web.Models.RealEstate;
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
        private readonly IRealestateAppService _agencyService;

        private readonly IPostAppService _postService;

        public AgenciesController(ICityAppService cityService, IPostAppService postService, IRealestateAppService agencyService)
        {
            _cityService = cityService;
            _postService = postService;
            _agencyService = agencyService;
        }
        public async Task<IActionResult> Index()
        {
            var cities = (await _cityService.GetAllAsync(new PagedAndSortedResultRequestDto { MaxResultCount = int.MaxValue })).Items.ToList();
            cities.Insert(0, new CityDto { Id = 0, Name = "شهر را انتخاب کنید" });
            ViewData["cities"] = cities;
            return View();
        }
        [HttpGet("[controller]/{id}")]
        public async Task<IActionResult> Show(int id, [FromQuery] int page = 1)
        {
            var agency = await _agencyService.GetAsync(new EntityDto<int> { Id = id });
            var posts = await _postService.GetPostsByAgency(new GetAllPostsInput()
            {
                RealEstateId = id,
                SkipCount = (page - 1) * 24,
                MaxResultCount = 24,

            });
            var model = new IndexViewMode { RealEstate = agency,Posts=posts.Items,
                HasNextPage = posts.TotalCount > (24 * page),
                Page = page,
                Total = posts.TotalCount
                
            };
            return View(model);
        }
    }
}
