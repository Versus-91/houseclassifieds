using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using classifieds.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMapFrom(typeof(Post))]
    public class PostDto:EntityDto,IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public Category Category { get; set; }
        public DateTime CreationTime { get ; set; }
    }
}
