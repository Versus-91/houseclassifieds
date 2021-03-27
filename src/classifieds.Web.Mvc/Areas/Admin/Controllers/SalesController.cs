using Abp.AspNetCore.Mvc.Controllers;
using classifieds.SaleReports.Dto;
using classifieds.Slaes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : AbpController
    {
        private readonly ISaleAppService _saleService;
        public SalesController(ISaleAppService saleService)
        {
            _saleService = saleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SaleReportDto inputs)
        {

            if (ModelState.IsValid)
            {
                await _saleService.CreateAsync(inputs);
                return Ok();
            }
            return BadRequest();
        }
    }
}
