
using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.Notification.Dto;
using classifieds.UserNotificationIds;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;

namespace classifieds.Notification
{
    public class NotificationAppService: AsyncCrudAppService<UserNotificationId, UserNotificationIdDto, int>
    {
        private readonly IFirebaseAppService _notificationManager;
        public NotificationAppService(IRepository<UserNotificationId> repository, IFirebaseAppService notificationManager) :base(repository)
        {
            DeletePermissionName = PermissionNames.Pages_Posts;
            GetAllPermissionName = PermissionNames.Pages_Posts;
            UpdatePermissionName = PermissionNames.Pages_Posts;
            _notificationManager = notificationManager;
        }
        public override async Task<UserNotificationIdDto> CreateAsync(UserNotificationIdDto input)
        {
            UserNotificationId userConnection =  await Repository.GetAll().FirstOrDefaultAsync(x => x.UserId == input.UserId);
            if (userConnection != null)
            {
                userConnection.FirebaseId = input.FirebaseId;
                await Repository.UpdateAsync(userConnection);
            }
            else
            {
                await Repository.InsertAsync(MapToEntity(input));
            }
            return MapToEntityDto(userConnection);

        }
    }
}
