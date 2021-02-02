using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using classifieds.Areas.Dto;
using classifieds.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Areas
{
    [AbpAuthorize(PermissionNames.Pages_Areas)]
    public class AreaAppService: AsyncCrudAppService<Area,AreaDto,int>,IAreaAppService
    {
        private readonly IRepository<Area> _repository;
        public AreaAppService(IRepository<Area> repository):base(repository)
        {
            _repository = repository;
        }
        protected override IQueryable<Area> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return base.CreateFilteredQuery(input)
                .Include(m => m.City);
        }
        [AbpAllowAnonymous]
        public async Task<ListResultDto<AreaDto>> GetAreaByCityId(int cityId)
        {
            var areas =await _repository.GetAllListAsync(m=>m.CityId == cityId);
            return new ListResultDto<AreaDto>(ObjectMapper.Map<List<AreaDto>>(areas));
        }
    }
}
