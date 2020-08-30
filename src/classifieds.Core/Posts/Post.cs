using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Categories;
using classifieds.Districts;
using classifieds.Images;
using classifieds.PostsAmenities;
using classifieds.PropertyTypes;
using System;
using System.Collections.Generic;

namespace classifieds.Posts
{
    public class Post : Entity, IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public DateTime CreationTime { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int TypeId { get; set; }
        public PropertyType Type { get; set; }
        public ushort Area { get; set; }
        public ushort Age { get; set; }
        public byte Bedroom { get; set; }
        public IList<PostAmenity> PostAmenities { get; set; }
        public uint View { get; set; }
        public double Deopsit { get; set; }
        public double Rent { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Post()
        {
            CreationTime = DateTime.Now;
        }
    }
}
