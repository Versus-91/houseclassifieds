using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Runtime.Validation;
using classifieds.Controllers;
using classifieds.Posts;
using classifieds.Web.Models.Ads;
using Microsoft.AspNetCore.Mvc;

namespace classifieds.Web.Controllers
{
    public class AdsController : classifiedsControllerBase
    {
        private readonly IPostAppService _postService;
        public AdsController(IPostAppService postService)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableValidation]
        public async Task<IActionResult> Create(AdsViewModel inputs)
        {
            if (ModelState.IsValid)
            {
                await _postService.CreateAsync(inputs.ToPost());
                return RedirectToAction("/");
            }
            else
            {
                return View();
            }
        }
    }
}
