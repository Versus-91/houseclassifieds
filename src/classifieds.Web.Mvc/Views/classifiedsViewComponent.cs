using Abp.AspNetCore.Mvc.ViewComponents;

namespace classifieds.Web.Views
{
    public abstract class classifiedsViewComponent : AbpViewComponent
    {
        protected classifiedsViewComponent()
        {
            LocalizationSourceName = classifiedsConsts.LocalizationSourceName;
        }
    }
}
