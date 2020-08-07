using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using classifieds.Categories;
using classifieds.Cities;
using classifieds.Posts.Dto;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace classifieds.Posts
{
    public class PostAppService:AsyncCrudAppService<Post,PostDto,int, GetAllPostsInput>,IPostAppService
    {
        private readonly IRepository<Post> _postRepository;
        public PostAppService(IRepository<Post> repository):base(repository)
        {
            _postRepository = repository;
        }

        protected override IQueryable<Post> CreateFilteredQuery(GetAllPostsInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.Featured.HasValue, t => t.IsFeatured == input.Featured.Value);
        }
        public async Task<PagedResultDto<DetailedPostsDto>> GetDetails(PagedAndSortedResultRequestDto input)
        {
                var items = await _postRepository.GetAllIncluding(m => m.District.City, m => m.Category)
                    .Skip(input.SkipCount).Take(input.MaxResultCount)

                    .Select(m => new DetailedPostsDto
                    {
                        Id = m.Id,
                        CityName = m.District.City.Name,
                        CategoryName = m.Category.Name
                    }).ToListAsync();
                return new PagedResultDto<DetailedPostsDto> { Items = items,TotalCount=items.Count()};
        }
    }
}
