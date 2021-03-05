using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using classifieds.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Authorization.Accounts.Dto
{
    [AutoMapFrom(typeof(User))]
    public class AccountUserDto : EntityDto<long>
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string EmailAddress { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsPhoneNumberConfirmed { get; set; }



    }
}
