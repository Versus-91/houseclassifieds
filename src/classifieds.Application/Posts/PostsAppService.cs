using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Posts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts
{
    public class PostsAppService:AsyncCrudAppService<Post,PostDto>,IPostsAppService
    {
        public PostsAppService(IRepository<Post> repository):base(repository)
        {

        }
    }
}
