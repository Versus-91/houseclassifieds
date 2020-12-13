using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Authorization.Accounts.Dto
{
    public class CheckUserInput
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public  string username { get; set; }
    }
}
