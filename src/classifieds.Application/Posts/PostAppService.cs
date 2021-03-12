 using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using classifieds.Amenities.Dto;
using classifieds.Authorization;
using classifieds.Authorization.Users;
using classifieds.Categories;
using classifieds.Cities;
using classifieds.Districts;
using classifieds.Districts.Dto;
using classifieds.Posts.Dto;
using classifieds.PostsAmenities;
using classifieds.PostsAmenities.Dto;
using classifieds.PropertyTypes;
using classifieds.Settings.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    public class PostAppService : AsyncCrudAppService<Post, PostDto, int, GetAllPostsInput, CreatePostInput, UpdatePostInput>, IPostAppService
    {
        private readonly ISettingManager _settingsManager;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<PropertyType> _typeRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Post>  _repository;
        private readonly IRepository<User, long> _userRepository;
        public PostAppService(ISettingManager settingsManager, IRepository<PropertyType> typeRepository,IRepository<City> cityRepository,IRepository<Post> repository, IRepository<Category> categoryRepository, IRepository<User, long> userRepository, IRepository<District> districtRepository) : base(repository)
        {
            _settingsManager = settingsManager;
            _typeRepository = typeRepository;
            _cityRepository = cityRepository;
            _postRepository = repository;
            _categoryRepository = categoryRepository;
            CreatePermissionName = PermissionNames.Posts_Create;
            // UpdatePermissionName = PermissionNames.Pages_Posts;
            _repository = repository;
             DeletePermissionName = PermissionNames.Pages_Posts;
            _districtRepository = districtRepository;
            _userRepository = userRepository;
        }
        public override Task DeleteAsync(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }
        [RemoteService(false)]
        public async Task<Post> GetPost(int id)
        {
            return await _repository.GetAsync(id);
        }

        protected override IQueryable<Post> CreateFilteredQuery(GetAllPostsInput input)
        {

            return base.CreateFilteredQuery(input)
                .Include(m=> m.District)
                   .ThenInclude(m=>m.Area)
                   .ThenInclude(m => m.City)
                .Include(m => m.Images)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .Include(m=>m.PostAmenities).ThenInclude(m=>m.Amenity)
                .Where(m=>m.IsVerified ==true)
                .WhereIf(input.Zone.HasValue, t => t.District.AreaId == input.Zone.Value)
                .WhereIf(input.HasMedia.HasValue, t => t.HasMedia == input.HasMedia.Value)
                .WhereIf(input.Featured.HasValue, t => t.IsFeatured == input.Featured.Value)
                .WhereIf(input.Category.HasValue, t => t.CategoryId == input.Category.Value)
                .WhereIf(input.District.HasValue, t => t.DistrictId == input.District.Value)
                .WhereIf(input.City.HasValue, t => t.District.Area.City.Id == input.City.Value)
                .WhereIf(input.Age.HasValue, t => t.Age >= input.Age.Value)
                .WhereIf(input.Beds.HasValue, t => t.Bedroom >= input.Beds.Value)
                .WhereIf(input.MinArea.HasValue && input.MaxArea.HasValue, t => t.Area >= input.MinArea.Value && t.Area <= input.MaxArea.Value)
                .WhereIf(input.MinPrice.HasValue && input.MaxPrice.HasValue, t => t.Price >= input.MinPrice.Value && t.Price <= input.MaxPrice.Value)
                .WhereIf(input.MinRent.HasValue && input.MaxRent.HasValue, t => t.Rent >= input.MinRent.Value && t.Rent <= input.MaxRent.Value)
                .WhereIf(input.MinDeposit.HasValue && input.MaxDeposit.HasValue, t => t.Deposit >= input.MinDeposit.Value && t.Deposit <= input.MaxDeposit.Value)
                .WhereIf(input.Amenities != null && input.Amenities.Count > 0, t => t.PostAmenities.Any(m=>input.Amenities.Contains(m.AmenityId)))
                .WhereIf(input.Types != null && input.Types.Count > 0, t => input.Types.Contains(t.TypeId))
                .WhereIf(input.UserId != null, t => t.CreatorUserId == input.UserId)
                .OrderByDescending(m => m.CreationTime);

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
                    DistrictId=m.DistrictId,
                    Price = m.Price,
                    Deposit = m.Deposit,
                    Rent = m.Rent,
                    Age = m.Age,
                    CategoryId =m.CategoryId,
                    TypeId=m.TypeId,
                    IsFeatured = m.IsFeatured,
                    Type = ObjectMapper.Map<TypeViewModel>(m.Type),
                    Description = m.Description,
                    Category = ObjectMapper.Map<CategoryViewModel>(m.Category),
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    District = ObjectMapper.Map<DistrictDto>(m.District),
                    Title = m.Title,
                    CreationTime = m.CreationTime,
                    Amenities = m.PostAmenities.Select(m=>new AmenityDto {
                        Name=m.Amenity.Name,
                        Icon= m.Amenity.Icon,
                        Id=m.Amenity.Id,
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
        public async Task<PagedResultDto<PostDto>> GetUserPosts(GetAllPostsInput input)
        {
            var posts = base.CreateFilteredQuery(input)
                .Include(m => m.District)
                .Include(m => m.District.Area)
                .Include(m => m.District.Area.City)
                .Include(m => m.Images)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .Include(m => m.PostAmenities).ThenInclude(m => m.Amenity).Where(m=>m.CreatorUserId == AbpSession.UserId).OrderByDescending(m=>m.CreationTime);
            var pagedPosts = ApplyPaging(posts, input);
             return new PagedResultDto<PostDto>(posts.Count(), ObjectMapper.Map<List<PostDto>>(await pagedPosts.ToListAsync()));
        }
        [RemoteService(false)]
        public async Task<PagedResultDto<PostDto>> GetPostsByUser(GetAllPostsInput input)
        {
            if (!input.UserId.HasValue)
            {
                throw new ArgumentException();
            }
            var posts = base.CreateFilteredQuery(input)
                .Include(m => m.District)
                .Include(m => m.District.Area.City)
                .Include(m => m.Images)
                .Include(m => m.Type)
                .Include(m => m.Category)
                .Include(m => m.PostAmenities).ThenInclude(m => m.Amenity).Where(m => m.CreatorUserId == input.UserId).OrderByDescending(m => m.CreationTime);
            var pagedPosts = ApplyPaging(posts, input);
            return new PagedResultDto<PostDto>(posts.Count(), ObjectMapper.Map<List<PostDto>>(await pagedPosts.ToListAsync()));
        }
        public async Task<PagedResultDto<PostDto>> Recommendations(PostDto post)
        {
            var items = await _postRepository.GetAllIncluding(m => m.District.Area.City, m => m.Category)
                .Where(m => m.IsVerified && m.Id != post.Id && m.CategoryId == post.CategoryId && m.DistrictId == post.DistrictId && m.TypeId == post.TypeId)
                .Select(m => new PostDto
                {
                    Id = m.Id,
                    Bedroom = m.Bedroom,
                    Area = m.Area,
                    Type = ObjectMapper.Map<TypeViewModel>(m.Type),
                    Description = m.Description,
                    DistrictId = m.DistrictId,
                    Price = m.Price,
                    Deposit = m.Deposit,
                    Rent = m.Rent,
                    CategoryId = m.CategoryId,
                    TypeId = m.TypeId,
                    Category = ObjectMapper.Map<CategoryViewModel>(m.Category),
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    District = ObjectMapper.Map<DistrictDto>(m.District),
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
                }).Take(4).ToListAsync();
            return new PagedResultDto<PostDto>(10, ObjectMapper.Map<List<PostDto>>(items));
        }
        [RemoteService(false)]
        public async Task<List<PostsCountDto>> CitiesPostsCount()
        {
  
             var counts = await _postRepository.GetAll().Where(m=> m.IsVerified == true).GroupBy(m => m.District.Area.CityId)
                .Select(n => new PostsCountDto
                {
                    CityId = n.Key,
                    CityName = _cityRepository.GetAll().Where(m => m.Id == n.Key).FirstOrDefault().Name,
                    Count = n.Count(),
                }
             ).OrderByDescending(m => m.Count).Take(20).ToListAsync();

            return counts;
        }
        [RemoteService(false)]
        public async Task<List<PostsCountDto>> PopularPosts()
        {
            var categories = await _categoryRepository.GetAllListAsync();
            var postsCountPerCategory = new List<CitiesPostsCount>();

            var counts = await _postRepository.GetAll().Where(m => m.IsVerified == true).GroupBy(m => m.District.Area.CityId)
               .Select(n => new PostsCountDto
               {
                   CityId = n.Key,
                   CityName = _cityRepository.GetAll().Where(m => m.Id == n.Key).FirstOrDefault().Name,
                   Count = n.Count(),
               }
            ).OrderByDescending(m => m.Count).Take(10).ToListAsync();

            return counts;
        }
        [RemoteService(false)]
        public async Task<List<UserPostsCountDto>> UsersPostsCount()
        {
            var usersPostsCount =await _userRepository.GetAll().Where(m=> m.Posts.Count > 0).Select(m=>new UserPostsCountDto
            {
                Avatar = m.Avatar,
                Name = m.FullName ,
                UserName = m.UserName,
                Id= m.Id,
                PostsCount = m.Posts.Where(m=>m.IsVerified == true)
                .Count() }).OrderByDescending(m=>m.PostsCount).Take(10).ToListAsync();

            return usersPostsCount;
       }
        [RemoteService(false)]
        public  async Task AddPostMedia(Post post,bool state)
        {
            post.HasMedia = state;
            await _postRepository.UpdateAsync(post);
        }
        public override async Task<PostDto> CreateAsync(CreatePostInput input)
        {
            List<PostAmenity> amenities=new List<PostAmenity>();
            if (input.Amenities != null && input.Amenities.Count > 0)
            {
                foreach (var amenity in input.Amenities)
                {
                    amenities.Add(new PostAmenity() { AmenityId = amenity });
                }
            }

            var post = ObjectMapper.Map<Post>(input);
            var category = await _categoryRepository.GetAsync(input.CategoryId);
            var type = await _typeRepository.GetAsync(input.TypeId);
            var district = await _districtRepository.GetAsync(input.DistrictId);
            var title = category?.Name + " " + type?.Name+" "+ input?.Area + " متری " + " " + district?.Name;
            if (!String.IsNullOrEmpty(title))
            {
                post.Title = title;
            }
            post.PostAmenities = amenities;   
            await _postRepository.InsertAndGetIdAsync(post);
            return ObjectMapper.Map<PostDto>(post);
        }
        public override async Task<PostDto> UpdateAsync(UpdatePostInput input)
        {
            List<PostAmenity> amenities = new List<PostAmenity>();
            if (input.Amenities != null && input.Amenities.Count > 0)
            {
                foreach (var amenity in input.Amenities)
                {
                    amenities.Add(new PostAmenity() { AmenityId = amenity,PostId = input.Id });
                }
            }
            var post =await _postRepository
                .GetAllIncluding(m => m.PostAmenities)
                .FirstOrDefaultAsync(m=>m.Id == input.Id);
            if (post == null || post.IsVerified == true)
            {
                throw new Exception();
            }
            post.CategoryId = input.CategoryId;
            post.DistrictId = input.DistrictId;
            post.Age = input.Age;
            post.Deposit = input.Deopsit;
            post.Description = input.Description;
            post.Area = input.Area;
            post.Bedroom = input.Bedroom;
            post.Latitude = input.Latitude;
            post.Longitude = input.Longitude;
            post.Price = input.Price;
            post.Rent = input.Rent;
            post.TypeId = input.TypeId;
            post.PostAmenities = amenities;
            await _postRepository.UpdateAsync(post);
            return MapToEntityDto(post);
        }
    }
}
