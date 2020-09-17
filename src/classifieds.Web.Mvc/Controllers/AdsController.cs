using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using classifieds.Categories;
using classifieds.Categories.Dto;
using classifieds.Controllers;
using classifieds.Districts;
using classifieds.Posts;
using classifieds.Posts.Dto;
using classifieds.PropertyTypes;
using classifieds.PropertyTypes.Dto;
using classifieds.Web.Models.Ads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace classifieds.Web.Controllers
{
    public class AdsController : classifiedsControllerBase
    {

        private readonly IPostAppService _postService;
        private readonly IDistrictAppService _districtService;
        private readonly ITypeAppService _typeService;
        private readonly ICategoryAppService _categoryService;
        public AdsController(
            IPostAppService postService,
            ICategoryAppService categoryService,
            IDistrictAppService districtService,
            ITypeAppService typeService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _districtService = districtService;
            _typeService = typeService;
        }
        public async Task<IActionResult> Index(GetAllPostsInput inputs)
        {
            ViewData["Categories"] = new SelectList((await _categoryService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, nameof(CategoryDto.Id), nameof(CategoryDto.Name),inputs.Category);
            ViewData["Districts"] = new SelectList((await _districtService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name");
            ViewData["PropertyTypes"] = new SelectList((await _typeService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, nameof(PropertyTypeDto.Id), nameof(PropertyTypeDto.Name),inputs.Type);

            return View(inputs);
        }
        [Route("[controller]/{id:int:required}")]
        public async Task<IActionResult> Show(int id)
        {
            var post = await _postService.GetDetails(id);
            if (post != null)
            {
                return View(post);
            }
            return NotFound();
        }
        public async Task<IActionResult> Create()
        {

            ViewData["Categories"] = new SelectList((await _categoryService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name");
            ViewData["Districts"] = new SelectList((await _districtService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name");
            ViewData["PropertyTypes"] = new SelectList((await _typeService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name");


            return View();
        }
        [HttpPost]
        [DisableValidation]

        public async Task<IActionResult> Create([FromBody] AdsViewModel inputs)
        {
            if (ModelState.IsValid)
            {
                var post = await _postService.CreateAsync(inputs.ToPost());
                return Json(post.Id);
            }
            else
            {

                ViewData["Categories"] = new SelectList((await _categoryService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name");
                ViewData["Districts"] = new SelectList((await _districtService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name");
                ViewData["PropertyTypes"] = new SelectList((await _typeService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name");
                return View();
            }
        }
    }
}
