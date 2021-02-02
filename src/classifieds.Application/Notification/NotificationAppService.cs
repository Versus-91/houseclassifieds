
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.Notification.Dto;
using classifieds.UserNotificationIds;
using Microsoft.EntityFrameworkCore;

namespace classifieds.Notification
{
    public class NotificationAppService: AsyncCrudAppService<UserNotificationId, UserNotificationIdDto, int>
    {
        public NotificationAppService(IRepository<UserNotificationId> repository):base(repository)
        {
            DeletePermissionName = PermissionNames.Pages_Posts;
            GetAllPermissionName = PermissionNames.Pages_Posts;
            UpdatePermissionName = PermissionNames.Pages_Posts;
        }
        public override async Task<UserNotificationIdDto> CreateAsync(UserNotificationIdDto input)
        {
            UserNotificationId userConnection =  await Repository.GetAll().FirstOrDefaultAsync(x => x.UserId == input.UserId);
            if (userConnection != null)
            {
                userConnection.FirebaseId = input.FirebaseId;
                await Repository.UpdateAsync(userConnection);
                return MapToEntityDto(userConnection);
            }
            else
            {
                await Repository.InsertAsync(MapToEntity(input));
                return MapToEntityDto(userConnection);
            }
        }
    }
}
