using Abp.Application.Services;
using classifieds.Districts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Districts
{
    public interface IDistrictAppService:IAsyncCrudAppService<DistrictDto>
    {
    }
}
