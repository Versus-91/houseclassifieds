using Abp.AspNetCore.Mvc.Controllers;
using classifieds.Posts;
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
        private readonly IPostAppService _postService;

        public SalesController(ISaleAppService saleService, IPostAppService postService)
        {
            _saleService = saleService;
            _postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult Create(int id)
        {
            return View();
        }
        public IActionResult Edit(int id)
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
