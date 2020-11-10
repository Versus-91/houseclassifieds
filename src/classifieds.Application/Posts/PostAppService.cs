using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using classifieds.Amenities.Dto;
using classifieds.Authorization;
using classifieds.Posts.Dto;
using classifieds.PostsAmenities.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                .Include(m=>m.PostAmenities)
                .Where(m=>m.IsVerified ==true)
                .WhereIf(input.Featured.HasValue, t => t.IsFeatured == input.Featured.Value)
                .WhereIf(input.Category.HasValue, t => t.CategoryId == input.Category.Value)
                .WhereIf(input.District.HasValue, t => t.DistrictId == input.District.Value)
                .WhereIf(input.City.HasValue, t => t.District.City.Id == input.City.Value)
                .WhereIf(input.Age.HasValue, t => t.Age >= input.Age.Value)
                .WhereIf(input.Beds.HasValue, t => t.Bedroom >= input.Beds.Value)
                .WhereIf(input.MinArea.HasValue && input.MaxArea.HasValue, t => t.Area > input.MinArea.Value && t.Area < input.MaxArea.Value)
                .WhereIf(input.Amenities != null && input.Amenities.Count > 0, t => t.PostAmenities.Any(m=>input.Amenities.Contains(m.AmenityId)))
                .WhereIf(input.Type != null && input.Type.Count > 0, t => input.Type.Contains(t.TypeId));
        }
        public async Task<PostDto> GetDetails(int id)
        {
            var item = await _postRepository.GetAllIncluding(m => m.District.City, m => m.Category).Where(m => m.Id == id)
                .Select(m => new PostDto
                {
                    Id = m.Id,
                    Bedroom = m.Bedroom,
                    Area = m.Area,
                    IsVerified = m.IsVerified,
                    IsFeatured = m.IsFeatured,
                    Type = ObjectMapper.Map<TypeViewModel>(m.Type),
                    Description = m.Description,
                    Category = ObjectMapper.Map<CategoryViewModel>(m.Category),
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    District = ObjectMapper.Map<DistrictViewModel>(m.District),
                    Title = m.Title,
                    CreationTime = m.CreationTime,
                    PostAmenities = m.PostAmenities.Select(m=>new PostAmenityDto {
                        Amenity = ObjectMapper.Map<AmenityDto>(m.Amenity),
                        Id=m.Id,
                    }).ToList(),
                    Images = m.Images.Select(m => new ImageViewModel
                    {
                        Id = m.Id,
                        Path = m.Path,
                        Name = m.Name
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return item;
        }
        [AbpAuthorize]
        public async Task<PagedResultDto<PostDto>> GetUserPosts()
        {
            var items = await _postRepository.GetAllIncluding(m => m.District.City, m => m.Category).Where(m => m.CreatorUserId == AbpSession.UserId)
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
                    IsFeatured = m.IsFeatured,
                    IsVerified = m.IsVerified,
                    CreationTime = m.CreationTime,
                    Images = m.Images.Select(m => new ImageViewModel
                    {
                        Id = m.Id,
                        Path = m.Path,
                        Name = m.Name
                    }).ToList(),
                }).ToListAsync();
             return new PagedResultDto<PostDto>(items.Count, ObjectMapper.Map<List<PostDto>>(items));
        }
    }
}
