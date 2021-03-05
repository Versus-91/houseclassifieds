using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Authorization;
using classifieds.ReportOptions;
using classifieds.ReportOptions.Dto;
using classifieds.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Amenities)]
    public class ReportOptionsController : AbpController
    {
        private readonly ReportOptionAppService _reportOptionService;


        public ReportOptionsController( ReportOptionAppService reportOptionService)
        {
            _reportOptionService = reportOptionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            var option =await _reportOptionService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", option);
        }

    }
}

