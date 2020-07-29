using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using classifieds.Authorization;

namespace classifieds
{
    [DependsOn(
        typeof(classifiedsCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class classifiedsApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<classifiedsAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(classifiedsApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
