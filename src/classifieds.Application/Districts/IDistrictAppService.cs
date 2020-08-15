using Abp.Application.Services;
using classifieds.Districts.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Districts
{
    public interface IDistrictAppService:IAsyncCrudAppService<DistrictDto>
    {
        Task<List<DistrictDto>> GetByCityId(int id);
    }
}
