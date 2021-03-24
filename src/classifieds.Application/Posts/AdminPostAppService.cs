using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using classifieds.Amenities.Dto;
using classifieds.Authorization;
using classifieds.Districts.Dto;
using classifieds.Notification;
using classifieds.Posts.Admin.Dto;
using classifieds.Posts.Dto;
using classifieds.UserNotificationIds;
using FirebaseAdmin.Messaging;

using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly IFirebaseAppService _notificationManager;
        private readonly IRepository<UserNotificationId>  _userFirebaseService;
        public AdminPostAppService(IRepository<Post> repository, IFirebaseAppService notificationManager
            , IRepository<UserNotificationId> userFirebaseService) : base(repository)
        {
            _notificationManager = notificationManager;
            _postRepository = repository;
            _userFirebaseService = userFirebaseService;
        }
        public override async Task<PostDto> UpdateAsync(AdminUpdatePostInput input)
        {
            Post post = _postRepository.Get(input.Id);
            if (post == null)
            {
                throw new ArgumentException();
            }
            else
            {
                if (post.IsVerified == false && input.IsVerified == true)
                {
                    var userFirebaseToken =await _userFirebaseService.GetAll().FirstOrDefaultAsync(x => x.UserId == post.CreatorUserId);
                    if (userFirebaseToken != null)
                    {
                        var notif = new FirebaseAdmin.Messaging.Notification();
                        if (string.IsNullOrEmpty(post.Title))
                        {
                            notif.Body = "اعلان شما با موفقیت تایید شد.";
                        }
                        else
                        {
                            notif.Body =post.Title +  "  با موفقیت تایید شد.";

                        }
                        notif.Title = "تایید اعلان";
                        var message = new Message()
                        {
                            Token = userFirebaseToken.FirebaseId,
                            Notification = notif
                        };
                        try
                        {
                            var result = await _notificationManager.SendMessage(message);
                            Logger.Info("notif result:" + result);
                        }
                        catch (Exception)
                        {
                        }
                    }

                }
                post.IsVerified = input.IsVerified;
                post.IsFeatured = input.IsFeatured;
                await _postRepository.UpdateAsync(post);
            }
            return MapToEntityDto(post);
        }
        protected override IQueryable<Post> CreateFilteredQuery(GetAllPostsInput input)
        {
            return base.CreateFilteredQuery(input)
                .Include(m=>m.District)
                    .ThenInclude(m=>m.Area)
                    .ThenInclude(m=>m.City)
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
                .WhereIf(input.Types != null && input.Types.Count > 0, t => input.Types.Contains(t.TypeId)).OrderByDescending(m=>m.CreationTime);
        }
        public async Task<PostDto> GetDetails(int id)
        {
            var item = await _postRepository.GetAllIncluding(m => m.District, m => m.Category, m => m.District.Area, m => m.District.Area.City).Where(m => m.Id == id)
                .Select(m => new PostDto
                {
                    Id = m.Id,
                    Bedroom = m.Bedroom,
                    Area = m.Area,
                    IsVerified = m.IsVerified,
                    DistrictId = m.DistrictId,
                    Price = m.Price,
                    Age = m.Age,
                    CategoryId = m.CategoryId,
                    TypeId = m.TypeId,
                    IsFeatured = m.IsFeatured,
                    Type = ObjectMapper.Map<TypeViewModel>(m.Type),
                    Description = m.Description,
                    Category = ObjectMapper.Map<CategoryViewModel>(m.Category),
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    District = ObjectMapper.Map<DistrictDto>(m.District),
                    Title = m.Title,
                    CreationTime = m.CreationTime,
                    Amenities = m.PostAmenities.Select(m => new AmenityDto
                    {
                        Name = m.Amenity.Name,
                        Icon = m.Amenity.Icon,
                        Id = m.Amenity.Id,
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
