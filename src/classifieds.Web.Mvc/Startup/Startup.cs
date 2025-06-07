using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Castle.Logging.Log4Net;
using Abp.Dependency;
using Abp.Json;
using Castle.Facilities.Logging;
using classifieds.Authentication.JwtBearer;
using classifieds.Configuration;
using classifieds.Identity;
using classifieds.Services;
using classifieds.Web.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SixLabors.ImageSharp.Web;
using SixLabors.ImageSharp.Web.Caching;
using SixLabors.ImageSharp.Web.Commands;
using SixLabors.ImageSharp.Web.DependencyInjection;
using SixLabors.ImageSharp.Web.Middleware;
using SixLabors.ImageSharp.Web.Processors;
using SixLabors.ImageSharp.Web.Providers;
using System;

namespace classifieds.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;
        private const string _apiVersion = "v1";

        public Startup(IWebHostEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(
                       options =>
                       {
                           options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                           options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                       }
                   )
                   .AddRazorRuntimeCompilation()
                   .AddNewtonsoftJson(options =>
                   {
                       options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                       {
                           NamingStrategy = new CamelCaseNamingStrategy()
                       };
                   });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.AddScoped<IWebResourceManager, WebResourceManager>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.Configure<SMSoptions>(_appConfiguration.GetSection("twillo"));
            services.AddSignalR();
            services.AddImageSharp()
                .SetRequestParser<QueryCollectionRequestParser>()
                .Configure<PhysicalFileSystemCacheOptions>(options =>
                {
                    
                    options.CacheFolder = "is-cache";
                })
                .SetCache(provider =>
                {
                    return new PhysicalFileSystemCache(
                                provider.GetRequiredService<IOptions<PhysicalFileSystemCacheOptions>>(),
                                provider.GetRequiredService<IWebHostEnvironment>(),
                                provider.GetRequiredService<IOptions<ImageSharpMiddlewareOptions>>(),
                                provider.GetRequiredService<FormatUtilities>());
                })
                .SetCacheHash<CacheHash>()
                .AddProvider<PhysicalFileSystemProvider>()
                .AddProcessor<helpers.ResizeWebProcessor>()
                .RemoveProcessor<SixLabors.ImageSharp.Web.Processors.ResizeWebProcessor>()
                .RemoveProcessor<FormatWebProcessor>()
                .RemoveProcessor<BackgroundColorWebProcessor>();
            // Configure Abp and Dependency Injection
            return services.AddAbp<classifiedsWebMvcModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                )
            ,removeConventionalInterceptors:false);

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseAbp(); // Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseImageSharp();


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseJwtTokenMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
