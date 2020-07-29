using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace classifieds.Web.Views
{
    public abstract class classifiedsRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected classifiedsRazorPage()
        {
            LocalizationSourceName = classifiedsConsts.LocalizationSourceName;
        }
    }
}
