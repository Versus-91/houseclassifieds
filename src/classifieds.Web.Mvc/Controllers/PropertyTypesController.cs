using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.PropertyTypes;
using Microsoft.AspNetCore.Mvc;

namespace classifieds.Web.Controllers
{
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
