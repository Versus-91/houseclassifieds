using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Categories.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Categories
{
    public class CategoryAppService:AsyncCrudAppService<Category,CategoryDto>,ICategoryAppService
    {
        public CategoryAppService(IRepository<Category> repository):base(repository)
        {

        }
        
    }
}
