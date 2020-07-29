using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Categories.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Categories
{
    public class CategoriesAppService:AsyncCrudAppService<Category,CategoryDto>,ICategoriesAppService
    {
        public CategoriesAppService(IRepository<Category> repository):base(repository)
        {

        }
        
    }
}
