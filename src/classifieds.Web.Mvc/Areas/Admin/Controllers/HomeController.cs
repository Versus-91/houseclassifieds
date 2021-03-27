using Abp.AspNetCore.Mvc.Controllers;
using Abp.Domain.Repositories;
using classifieds.Authorization.Users;
using classifieds.Posts;
using classifieds.Web.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : AbpController
    {
        private readonly IPostAppService _postService;

        public HomeController(IPostAppService postService)
        {
            _postService = postService;
        }
        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel();
            model.ProcessorUsage = await CpuUsage();
            model.PostsTotalNumber =await _postService.PostsCount();
            model.SalesTotal = 125;
            model.UsersTotalNumber = 589;

            return View(model);
        }

        private async Task<int> CpuUsage()
        {
            var currentProcessName = Process.GetCurrentProcess().ProcessName;
            var cpuCounter = new PerformanceCounter("Process", "% Processor Time", currentProcessName);
            cpuCounter.NextValue();
            await Task.Delay(500);
            return (int)cpuCounter.NextValue();
        }
    }
}
