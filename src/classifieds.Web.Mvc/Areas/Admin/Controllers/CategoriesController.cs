using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Authorization;
using classifieds.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Categories)]
    public class CategoriesController : AbpController
    {
        private readonly ICategoryAppService _categoryService;

        public CategoriesController(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _categoryService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", model);
        }
    }
}