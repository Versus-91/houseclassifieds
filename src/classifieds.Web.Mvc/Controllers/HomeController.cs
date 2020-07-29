using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using classifieds.Controllers;

namespace classifieds.Web.Controllers
{
    public class HomeController : classifiedsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }
    }
}
