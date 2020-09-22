using Abp.Application.Services;
using Abp.Application.Services.Dto;
using classifieds.Posts.Admin.Dto;
using classifieds.Posts.Dto;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    public interface IPostAppService : IAsyncCrudAppService<PostDto, int, GetAllPostsInput, CreatePostInput, UpdatePostInput>
    {
        Task<PostDto> GetDetails(int id);
        Task<PagedResultDto<PostDto>> GetUserPosts();
    }
}
