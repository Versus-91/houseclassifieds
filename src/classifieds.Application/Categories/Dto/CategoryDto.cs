using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Categories.Dto
{
    [AutoMap(typeof(Category))]
    public class CategoryDto:EntityDto,IHasCreationTime
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }

    }
}
