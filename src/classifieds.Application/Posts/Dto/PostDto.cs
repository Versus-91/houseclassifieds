using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using classifieds.Categories;
using classifieds.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Post))]
    public class PostDto:EntityDto,IHasCreationTime
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsFeatured { get; set; } = false;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ushort Area { get; set; }
        public byte Bedroom { get; set; }
        public City City { get; set; }
        public int DistrictId { get; set; }
        public DateTime CreationTime { get ; set; }
    }
}
