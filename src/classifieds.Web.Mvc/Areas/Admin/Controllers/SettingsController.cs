using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Configuration;
using classifieds.Authorization;
using classifieds.Cities;
using classifieds.Web.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AbpMvcAuthorize(PermissionNames.Pages_Settings)]
    public class SettingsController : AbpController
    {
        private readonly ISettingManager _settingManager;


        public SettingsController( ISettingManager settingManager)
        {
               _settingManager = settingManager;
        }
        public async Task<ActionResult> Index()
        {
            var settings = await _settingManager.GetAllSettingValuesAsync() ;
            return View(settings);
        }
        public async Task<ActionResult> Edit(string name)
        {
            var value =await  _settingManager.GetSettingValueAsync(name);
            return View(new SettingsInput { Name=name,Value= value });
        }
        [HttpPost]
        public async Task<ActionResult> Edit(SettingsInput input)
        {
            await _settingManager.ChangeSettingForApplicationAsync(input.Name,input.Value);
            return RedirectToAction("Index");
        }
    }
}
