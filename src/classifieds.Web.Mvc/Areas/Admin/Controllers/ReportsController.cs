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
    public class ReportsController : AbpController
    {
        private readonly ReportAppService _reportService;
        private readonly ReportOptionAppService _reportOptionService;


        public ReportsController(ReportAppService reportService, ReportOptionAppService reportOptionService)
        {
            _reportService = reportService;
            _reportOptionService = reportOptionService;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["Options"] = new SelectList((await _reportOptionService.GetAllAsync(new PagedAndSortedResultRequestDto())).Items, nameof(ReportOptionDto.Id), nameof(ReportOptionDto.Name));
            return View();
        }
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _reportService.GetAsync(new EntityDto(id));
            return PartialView("_EditModal", model);
        }
    }
}

