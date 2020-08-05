using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Cities;
using classifieds.Cities.Dto;
using classifieds.Posts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace classifieds.Web.Controllers
{
    public class PostsController : AbpController
    {
        private readonly IPostAppService _postService;
        public PostsController(IPostAppService postService)
        {
            _postService = postService;
        }
        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }

    }
}
