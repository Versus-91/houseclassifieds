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
        Task<List<LocationSearchDto>> Find(string query);
        Task<List<DistrictDto>> GetByCityId(int id);
        Task<DistrictDto> GetById(int id);
        Task<string> LocationLabel(int id);
    }
}
