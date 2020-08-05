using Abp.Application.Services.Dto;
using classifieds.Categories;
using classifieds.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Posts.Dto
{
    public class DetailedPostsDto:EntityDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsFeatured { get; set; } = false;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CityName { get; set; }
        public int DistrictId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
