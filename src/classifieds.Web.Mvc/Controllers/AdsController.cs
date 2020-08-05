using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Validation;
using classifieds.Categories;
using classifieds.Controllers;
using classifieds.Districts;
using classifieds.Images;
using classifieds.Posts;
using classifieds.Web.Models.Ads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace classifieds.Web.Controllers
{
    public class AdsController : classifiedsControllerBase
    {
        private readonly IPostAppService _postService;
        private readonly IDistrictAppService _districtService;
        private readonly ICategoryAppService _categoryService;
        private readonly IHostEnvironment _environment;
        private readonly IRepository<Image> _imageService;
        public AdsController(IPostAppService postService, ICategoryAppService categoryService, 
            IDistrictAppService districtService, IHostEnvironment environment
            , IRepository<Image> imageService)
        {
            _postService = postService;
            _categoryService = categoryService;
            _districtService = districtService;
            _environment = environment;
            _imageService = imageService;
        }
        public IActionResult Index(int id)
        {
            int num = id;
            return View();
        }
        [Route("[controller]/{id:int:required}")]
        public IActionResult Show(int id)
        {
            int num = id;
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var model = new CreateAdViewModel
            {
                Categories = new SelectList((await _categoryService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name"),
                Districts = new SelectList((await _districtService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name")
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableValidation]
        public async Task<IActionResult> Create([Bind(Prefix = "Ad")] AdsViewModel inputs)
        {
            if (ModelState.IsValid)
            {
                await _postService.CreateAsync(inputs.ToPost());
                var path = Path.Combine(_environment.ContentRootPath, "Images");
                foreach (var file in inputs.Files)
                {
                    using (var stream = new FileStream(Path.Combine(path,file.FileName),FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    await _imageService.InsertAsync(new Image{
                        Name=file.Name,
                        Path= Path.Combine(path, file.FileName),
                        Size = file.Length
                    });
                }
                return RedirectToAction("index");
            }
            else
            {
                var model = new CreateAdViewModel
                {
                    Categories = new SelectList((await _categoryService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name"),
                    Districts = new SelectList((await _districtService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, "Id", "Name")
                };
                return View(model);
            }
        }
    }
}
