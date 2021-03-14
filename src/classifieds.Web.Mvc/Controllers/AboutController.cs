using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using classifieds.Controllers;
using Abp.Configuration;
using System.Threading.Tasks;
using classifieds.Settings.Constants;
using classifieds.Web.Models.Settings;

namespace classifieds.Web.Controllers
{
    public class AboutController : classifiedsControllerBase
    {
        private readonly ISettingManager _settingsManager;
        public AboutController(ISettingManager settingsManager)
        {
            _settingsManager = settingsManager;
        }
        public async Task<ActionResult> Index()
        {
            var aboutText = await _settingsManager.GetSettingValueAsync(SiteSettings.About);
            var setting = new SettingsInput { Value = aboutText };
            return View(setting);
        }
	}
}
