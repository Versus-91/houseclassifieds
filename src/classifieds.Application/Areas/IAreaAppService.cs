using Abp.Application.Services;
using Abp.Application.Services.Dto;
using classifieds.Areas.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Areas
{
    public interface IAreaAppService: IAsyncCrudAppService<AreaDto>
    {
        Task<ListResultDto<AreaDto>> GetAreaByCityId(int cityId);
    }
}
