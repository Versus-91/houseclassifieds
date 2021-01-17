using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using classifieds.Posts.Dto;
using classifieds.Users.Dto;
using Microsoft.AspNetCore.Identity;

namespace classifieds.Models.ManageViewModels
{
    public class IndexViewModel
    {
        public UserDto User { get; set; }
        public int Page { get; set; }
        public bool HasPassword { get; set; }
        public bool HasNextPage{ get; set; }

        public IList<UserLoginInfo> Logins { get; set; }
        public IReadOnlyList<PostDto> Posts { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}
