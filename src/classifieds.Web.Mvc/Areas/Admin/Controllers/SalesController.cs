using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : AbpController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
