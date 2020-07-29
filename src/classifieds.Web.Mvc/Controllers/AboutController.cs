using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using classifieds.Controllers;

namespace classifieds.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : classifiedsControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}
