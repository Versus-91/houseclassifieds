using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Posts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts
{
    public class PostAppService:AsyncCrudAppService<Post,PostDto>,IPostAppService
    {
        public PostAppService(IRepository<Post> repository):base(repository)
        {

        }
    }
}
