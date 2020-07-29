using Abp.Application.Services;
using classifieds.Provinces.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Provinces
{
    public interface IProvincesAppService : IAsyncCrudAppService<ProvinceDto>
    {
    }
}
