using Abp.Domain.Entities;
using classifieds.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.UserNotificationIds
{
    public class UserNotificationId:Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string FirebaseId { get; set; }

    }
}
