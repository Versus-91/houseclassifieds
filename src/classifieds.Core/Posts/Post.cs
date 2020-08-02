using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Categories;
using classifieds.Districts;
using classifieds.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts
{
    public class Post:Entity,IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public Post()
        {
            CreationTime = DateTime.Now;
        }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public DateTime CreationTime { get ; set ; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
