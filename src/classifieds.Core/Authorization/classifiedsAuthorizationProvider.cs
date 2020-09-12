using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace classifieds.Authorization
{
    public class classifiedsAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Posts_Create, L("Create Post"));
            context.CreatePermission(PermissionNames.Pages_Cities, L("Cities"));
            context.CreatePermission(PermissionNames.Pages_District, L("Districts"));
            context.CreatePermission(PermissionNames.Pages_PropertyTypes, L("PropertyTypes"));
            context.CreatePermission(PermissionNames.Pages_Categories, L("Categories"));
            context.CreatePermission(PermissionNames.Pages_Posts, L("Categories"));
            context.CreatePermission(PermissionNames.Pages_AdminPosts, L("AdminPosts"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, classifiedsConsts.LocalizationSourceName);
        }
    }
}
