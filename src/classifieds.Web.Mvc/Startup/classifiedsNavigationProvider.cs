using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using classifieds.Authorization;

namespace classifieds.Web.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// </summary>
    public class classifiedsNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu

                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Home,
                        L("HomePage"),
                        url: "",
                        order: 1,
                        icon: "fas fa-home"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Reports,
                        L("Reports"),
                        url: "admin/reports",
                        order: 1,
                        icon: "fas fa-home"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.ReportOptions,
                        L("ReportOptions"),
                        url: "admin/reportoptions",
                        order: 1,
                        icon: "fas fa-home"
                    )
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Amenities,
                        L("Amenities"),
                        url: "admin/amenities",
                        order: 1,
                        icon: "fas fa-home"
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.About,
                        L("About"),
                        url: "About",
                        order: 2,
                        icon: "fas fa-info-circle"
                    )
                ).AddItem(
                new MenuItemDefinition(
                        PageNames.Posts,
                        L("Posts"),
                        url: "admin/Posts",
                        icon: "fas fa-sticky-note",
                        order: 0,
                        requiresAuthentication: true)
                )
                .AddItem(
                new MenuItemDefinition(
                        PageNames.Categories,
                        L("Categories"),
                        icon: "fas fa-home",
                        order: 3,
                        requiresAuthentication: true).AddItem(
                new MenuItemDefinition(
                        PageNames.PropertyTypes,
                        L("PropertyTypes"),
                        url: "admin/PropertyTypes",
                        icon: "fas fa-home",
                        requiresAuthentication: true)
                )
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.Categories,
                        L("Categories"),
                        url: "admin/Categories",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                  )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Authorization,
                        L("Authorization"),
                        order: 4,
                        icon: "fas fa-lock"
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Tenants,
                        L("Tenants"),
                        url: "admin/Tenants",
                        icon: "fas fa-building",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Tenants)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Users,
                        L("Users"),
                        url: "admin/Users",
                        icon: "fas fa-users",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Users)
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Roles,
                        L("Roles"),
                        url: "admin/Roles",
                        icon: "fas fa-theater-masks",
                        permissionDependency: new SimplePermissionDependency(PermissionNames.Pages_Roles)
                            )
                )
                )
                .AddItem( // Menu items below is just for demonstration!
                    new MenuItemDefinition(
                        "MultiLevelMenu",
                        L("Location"),
                        order: 5,
                        icon: "fas fa-map"
                    ).AddItem(
                    new MenuItemDefinition(
                        PageNames.Cities,
                        L("Cities"),
                        url: "admin/Cities",
                        icon: "fas fa-home",
                        requiresAuthentication: true
                    )
                )
                .AddItem(
                new MenuItemDefinition(
                        PageNames.Districts,
                        L("Districts"),
                        url: "admin/Districts",
                        icon: "fas fa-home",
                        requiresAuthentication: true)
                ).AddItem(
                new MenuItemDefinition(
                        PageNames.Areas,
                        L("Areas"),
                        url: "admin/Areas",
                        icon: "fas fa-home",
                        requiresAuthentication: true)
                )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, classifiedsConsts.LocalizationSourceName);
        }
    }
}