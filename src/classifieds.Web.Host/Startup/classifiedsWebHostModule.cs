using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using classifieds.Configuration;

namespace classifieds.Web.Host.Startup
{
    [DependsOn(
       typeof(classifiedsWebCoreModule))]
    public class classifiedsWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public classifiedsWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(classifiedsWebHostModule).GetAssembly());
        }
    }
}
