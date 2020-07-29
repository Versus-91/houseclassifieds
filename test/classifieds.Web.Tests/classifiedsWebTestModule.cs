using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using classifieds.EntityFrameworkCore;
using classifieds.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace classifieds.Web.Tests
{
    [DependsOn(
        typeof(classifiedsWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class classifiedsWebTestModule : AbpModule
    {
        public classifiedsWebTestModule(classifiedsEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(classifiedsWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(classifiedsWebMvcModule).Assembly);
        }
    }
}