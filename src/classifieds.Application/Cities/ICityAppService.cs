using Abp.Application.Services;
using classifieds.Cities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Cities
{
    public interface ICityAppService:IAsyncCrudAppService<CityDto>
    {
    }
}
