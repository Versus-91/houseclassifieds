using Abp.Application.Services;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using classifieds.Authorization;
using classifieds.Images;
using classifieds.Posts.Dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    public class PostAppService : AsyncCrudAppService<Post, PostDto, int, GetAllPostsInput, CreatePostInput, UpdatePostInput>, IPostAppService
    {
        private readonly IRepository<Post> _postRepository;
        public PostAppService(IRepository<Post> repository) : base(repository)
        {
            _postRepository = repository;
            CreatePermissionName = PermissionNames.Pages_Posts;
            UpdatePermissionName = PermissionNames.Pages_Posts;
            DeletePermissionName = PermissionNames.Pages_Posts;
        }

        protected override IQueryable<Post> CreateFilteredQuery(GetAllPostsInput input)
        {
            return base.CreateFilteredQuery(input)
                .Include(m=> m.District.City)
                .Include(m => m.Images)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .Where(m=>m.IsVerified)
                .WhereIf(input.Featured.HasValue, t => t.IsFeatured == input.Featured.Value)
                //.WhereIf(input.MinPrice.HasValue && input.MaxPrice.HasValue, t => t. == input.Featured.Value)
                .WhereIf(input.Category.HasValue, t => t.CategoryId == input.Category.Value)
                .WhereIf(input.District.HasValue, t => t.DistrictId == input.District.Value)
                .WhereIf(input.City.HasValue, t => t.District.City.Id == input.City.Value)
                .WhereIf(input.Age.HasValue, t => t.Age == input.Age.Value)
                .WhereIf(input.Beds.HasValue, t => t.Bedroom == input.Beds.Value)
                .WhereIf(input.MinArea.HasValue && input.MaxArea.HasValue, t => t.Area > input.MinArea.Value && t.Area < input.MaxArea.Value)
                .WhereIf(input.Type.HasValue, t => t.TypeId == input.Type.Value);
        }
        public async Task<PostDto> GetDetails(int id)
        {
            var item = await _postRepository.GetAllIncluding(m => m.District.City, m => m.Category).Where(m => m.Id == id)
                .Select(m => new PostDto
                {
                    Id = m.Id,
                    Bedroom = m.Bedroom,
                    Area = m.Area,
                    Type = ObjectMapper.Map<TypeViewModel>(m.Type),
                    Description = m.Description,
                    Category = ObjectMapper.Map<CategoryViewModel>(m.Category),
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    District = ObjectMapper.Map<DistrictViewModel>(m.District),
                    Title = m.Title,
                    CreationTime = m.CreationTime,
                    Images = m.Images.Select(m => new ImageViewModel
                    {
                        Id = m.Id,
                        Path = m.Path,
                        Name = m.Name
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return item;
        }
    }
}
