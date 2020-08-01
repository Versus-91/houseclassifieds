using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using classifieds.Categories.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Categories
{
    public class CategoryService:classifiedsAppServiceBase, ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ListResultDto<CategoryDto>> GetAll()
        {
            var categories = await _categoryRepository.GetAll().ToListAsync();
            return new ListResultDto<CategoryDto>(ObjectMapper.Map<List<CategoryDto>>(categories)) ;
        }
    }
}
