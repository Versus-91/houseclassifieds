using Abp.Application.Services;
using Abp.Application.Services.Dto;
using classifieds.Posts.Admin.Dto;
using classifieds.Posts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    public interface IAdminPostAppService : IAsyncCrudAppService<PostDto, int, GetAllPostsInput, CreatePostInput, AdminUpdatePostInput>
    {
     Task<PostDto> GetDetails(int id);
     Task<PagedResultDto<PostDto>> GetAllDetails(GetAllPostsInput input);
}
}
