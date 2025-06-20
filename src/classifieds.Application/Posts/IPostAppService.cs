﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using classifieds.Posts.Admin.Dto;
using classifieds.Posts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    public interface IPostAppService : IAsyncCrudAppService<PostDto, int, GetAllPostsInput, CreatePostInput, UpdatePostInput>
    {
        Task<PostDto> GetDetails(int id);
        Task<PagedResultDto<PostDto>> GetUserPosts(GetAllPostsInput input);
        Task<PagedResultDto<PostDto>> Recommendations(PostDto post);
        Task<List<PostsCountDto>> CitiesPostsCount();
        Task<List<UserPostsCountDto>> UsersPostsCount();
        Task<List<PostsCountDto>> PopularPosts();
        Task<Post> GetPost(int id);
        Task AddPostMedia(Post post, bool state);
        Task<PagedResultDto<PostDto>> GetPostsByUser(GetAllPostsInput input);
        Task<int> PostsCount();
        Task<PagedResultDto<PostDto>> GetPostsByAgency(GetAllPostsInput input);

    }
}
