using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Views.Shared.Components.CustomPager
{
    public class CustomPagerViewComponent: classifiedsViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult(View() as IViewComponentResult);
        }
    }
}
