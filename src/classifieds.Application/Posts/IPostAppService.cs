using Abp.Application.Services;
using classifieds.Posts.Dto;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    public interface IPostAppService : IAsyncCrudAppService<PostDto, int, GetAllPostsInput, CreatePostInput, UpdatePostInput>
    {
        Task<PostDto> GetDetails(int id);
    }
}
