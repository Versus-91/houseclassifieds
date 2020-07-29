using Abp.Authorization;
using classifieds.Authorization.Roles;
using classifieds.Authorization.Users;

namespace classifieds.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
