using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Authorization.Users;
using classifieds.Posts;
using classifieds.Posts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Favorites.Dto
{
    [AutoMap(typeof(Favorite))]
    public class FavoriteDto : AuditedEntityDto
    {
        public int PostId { get; set; }
        public PostDto Post { get; set; }
    }
}
