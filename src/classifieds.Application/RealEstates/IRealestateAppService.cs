using Abp.Application.Services;
using classifieds.RealEstates.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace classifieds.RealEstates
{
    public interface IRealestateAppService : IAsyncCrudAppService<RealEstateDto,int, GetRealEstatesInput>
    {
        Task<List<RealEstateDto>> Find(string term);
    }
}
