using System.Collections.Generic;
using classifieds.Roles.Dto;

namespace classifieds.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
