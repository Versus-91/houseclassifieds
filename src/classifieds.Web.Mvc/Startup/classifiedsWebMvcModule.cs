using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using classifieds.Configuration;
using classifieds.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;

namespace classifieds.Web.Startup
{
    [DependsOn(typeof(classifiedsWebCoreModule))]
    public class classifiedsWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public classifiedsWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            IocManager.Register<IKeyVaultService, KeyVaultService>(DependencyLifeStyle.Transient);
            var keyVaultService = IocManager.Resolve<IKeyVaultService>();
            var connectionString = keyVaultService.GetSecretAsync("sqlconnection").GetAwaiter().GetResult();
            Configuration.DefaultNameOrConnectionString = connectionString.Value;

            Configuration.Navigation.Providers.Add<classifiedsNavigationProvider>();
        }

        public override void Initialize()
        {

            IocManager.RegisterAssemblyByConvention(typeof(classifiedsWebMvcModule).GetAssembly());
        }
    }
}
