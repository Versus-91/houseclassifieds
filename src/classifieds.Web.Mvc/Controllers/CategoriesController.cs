using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Authorization;
using classifieds.Categories;
using classifieds.Categories.Dto;
using classifieds.Web.Models.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace classifieds.Web.Controllers
{
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
