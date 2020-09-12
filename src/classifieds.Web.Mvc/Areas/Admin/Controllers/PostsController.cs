using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Runtime.Validation;
using classifieds.Authorization;
using classifieds.Cities;
using classifieds.Cities.Dto;
using classifieds.Posts;
using classifieds.Posts.Admin.Dto;
using classifieds.Posts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_AdminPosts)]
    public class PostsController : AbpController
    {
        private readonly IAdminPostAppService _postService;
        public PostsController(IAdminPostAppService postService)
        {
            _postService = postService;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }
        [Route("[area]/[controller]/{id:int:required}")]
        public async Task<ActionResult> Show(int id)
        {
            var model =await _postService.GetDetails(id);
            return View(model);
        }
        [HttpPost]
        [DisableValidation]

        public async Task<IActionResult> Update( AdminUpdatePostInput inputs)
        {
            if (ModelState.IsValid)
            {
                await _postService.UpdateAsync(inputs);
                return RedirectToAction("index", "posts", new { @area = "Admin" });
            }else
            {
                var model = await _postService.GetDetails(inputs.Id);
                return View("show", model);
            }
        }
    }
}
