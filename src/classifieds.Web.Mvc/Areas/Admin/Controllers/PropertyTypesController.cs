using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Authorization;
using classifieds.PropertyTypes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Cities)]
    public class PropertyTypesController : AbpController
    {
        private readonly ITypeAppService _typeService;

        public PropertyTypesController(ITypeAppService typeService)
        {
            _typeService = typeService;
        }

        // GET: CategoriesController
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await _typeService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", model);
        }
    }
}