using Abp.Application.Services;
using classifieds.Posts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts
{
    public interface IPostAppService:IAsyncCrudAppService<PostDto>
    {
    }
}
