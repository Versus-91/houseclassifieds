using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace classifieds.Controllers
{
    public abstract class classifiedsControllerBase: AbpController
    {
        protected classifiedsControllerBase()
        {
            LocalizationSourceName = classifiedsConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
