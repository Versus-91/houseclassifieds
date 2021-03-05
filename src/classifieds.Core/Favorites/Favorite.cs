using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Authorization.Users;
using classifieds.Posts;

namespace classifieds.Favorites
{
    public class Favorite : AuditedEntity
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
