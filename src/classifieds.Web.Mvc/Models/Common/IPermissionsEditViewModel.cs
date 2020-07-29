using System.Collections.Generic;
using classifieds.Roles.Dto;

namespace classifieds.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}