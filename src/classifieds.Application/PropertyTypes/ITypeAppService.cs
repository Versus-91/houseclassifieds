using Abp.Application.Services;
using classifieds.PropertyTypes.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.PropertyTypes
{
    public interface ITypeAppService:IAsyncCrudAppService<PropertyTypeDto>
    {
    }
}
