using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using classifieds.Controllers;
using classifieds.Posts;
using Microsoft.AspNetCore.Mvc;

namespace classifieds.Web.Controllers
{
    public class AdsController : classifiedsControllerBase
    {
        private readonly IPostsAppService _postService;
        public AdsController(IPostsAppService postService)
        {
            _postService = postService;
        }
        public IActionResult Index(int id)
        {
            int num = id ;
            return View();
        }
        [Route("[controller]/{id:int:required}")]
        public IActionResult Show(int id)
        {
            int num = id;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
