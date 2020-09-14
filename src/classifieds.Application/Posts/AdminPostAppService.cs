using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using classifieds.Authorization;
using classifieds.Images;
using classifieds.Posts.Admin.Dto;
using classifieds.Posts.Dto;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    [AbpAuthorize(PermissionNames.Pages_AdminPosts)]
    public class AdminPostAppService : AsyncCrudAppService<Post, PostDto, int, GetAllPostsInput, CreatePostInput, AdminUpdatePostInput>, IAdminPostAppService
    {
        private readonly IRepository<Post> _postRepository;
        public AdminPostAppService(IRepository<Post> repository) : base(repository)
        {
            _postRepository = repository;
        }

        protected override IQueryable<Post> CreateFilteredQuery(GetAllPostsInput input)
        {
            return base.CreateFilteredQuery(input)
                .Include(m=>m.District.City)
                .Include(m=>m.Category)
                .Include(m => m.Category)
                .Include(m=>m.Type)
                .WhereIf(input.Featured.HasValue, t => t.IsFeatured == input.Featured.Value)
                //.WhereIf(input.MinPrice.HasValue && input.MaxPrice.HasValue, t => t. == input.Featured.Value)
                .WhereIf(input.Category.HasValue, t => t.CategoryId == input.Category.Value)
                .WhereIf(input.District.HasValue, t => t.DistrictId == input.District.Value)
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
                    Description = m.Description,
                    Type = ObjectMapper.Map<TypeViewModel>(m.Type),
                    Category = ObjectMapper.Map<CategoryViewModel>(m.Category),
                    District = ObjectMapper.Map<DistrictViewModel>(m.District),
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    Title = m.Title,
                    CreationTime = m.CreationTime,
                    IsVerified = m.IsVerified,
                    IsFeatured = m.IsFeatured,
                    Images = m.Images.Select(m => new ImageViewModel
                    {
                        Id = m.Id,
                        Path = m.Path,
                        Name = m.Name
                    }).ToList(),
                }).FirstOrDefaultAsync();
            return item;
        }
        //public async Task<PagedResultDto<PostDto>> GetAllDetails(GetAllPostsInput input)
        //{
        //    var query = _postRepository.GetAllIncluding(m => m.District.City, m => m.Category);
        //    var totalCount = _postRepository.GetAllIncluding(m => m.District.City, m => m.Category).Count();
        //    query=ApplyPaging(query, input);
        //    query=ApplySorting(query, input);
        //    var posts = await query.Select(m => new PostDto
        //        {
        //            Id = m.Id,
        //            Bedroom = m.Bedroom,
        //            Area = m.Area,
        //            Description = m.Description,
        //            Category = m.Category.Name,
        //            //District = m.District.Name,
        //            Latitude = m.Latitude,
        //            Longitude = m.Longitude,
        //            Title = m.Title,
        //            CreationTime = m.CreationTime,
        //            //City = m.District.City.Name,
        //            IsVerified = m.IsVerified,
        //            IsFeatured = m.IsFeatured,
        //        }).ToListAsync();
        //    return new PagedResultDto<PostDto>(totalCount, ObjectMapper.Map<List<PostDto>>(posts));
        //}
    }
}
