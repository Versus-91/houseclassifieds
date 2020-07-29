using System.Collections.Generic;
using classifieds.Roles.Dto;

namespace classifieds.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
