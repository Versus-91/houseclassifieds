using Abp.Application.Services;
using Abp.Application.Services.Dto;
using classifieds.Categories.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Categories
{
    public interface ICategoryService:IApplicationService
    {
        Task<ListResultDto<CategoryDto>> GetAll();
    }
}
