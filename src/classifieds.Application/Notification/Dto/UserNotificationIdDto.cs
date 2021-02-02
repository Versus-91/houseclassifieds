using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.UserNotificationIds;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Notification.Dto
{
    [AutoMap(typeof(UserNotificationId))]
    public class UserNotificationIdDto:EntityDto
    {
        public int UserId { get; set; }
        public string FirebaseId { get; set; }
    }
}
