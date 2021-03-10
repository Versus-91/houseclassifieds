using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Threading.BackgroundWorkers;
using classifieds.Amenities.Dto;
using classifieds.Authorization;
using classifieds.Jobs;
using classifieds.Posts;
using classifieds.Posts.Dto;
using classifieds.PostsAmenities.Dto;
using classifieds.Settings;
using System.Linq;

namespace classifieds
{
    [DependsOn(
        typeof(classifiedsCoreModule),
        typeof(AbpAutoMapperModule))]
    public class classifiedsApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Settings.Providers.Add<ClassifiedSettigsProvider>();
            Configuration.Authorization.Providers.Add<classifiedsAuthorizationProvider>();
            Configuration.MultiTenancy.IsEnabled = false;
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<Post, PostDto>()
                      .ForMember(u => u.Amenities, options => options.MapFrom(input => input.PostAmenities.Select(m=>m.Amenity)));
    
            });

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
        public override void PostInitialize()
        {
            var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            workManager.Add(IocManager.Resolve<PostsJob>());
        }
    }
}
