using Abp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Authorization.Accounts.Dto
{
    public class CheckPhoneInput
    {
        [Required]
        [StringLength(AbpUserBase.MaxSurnameLength)]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
