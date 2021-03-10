using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Authorization.Users;
using classifieds.Categories;
using classifieds.Districts;
using classifieds.Images;
using classifieds.PostsAmenities;
using classifieds.PropertyTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace classifieds.Posts
{
    public class Post :AuditedEntity,IHasCreationTime
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
        public int TypeId { get; set; }
        public PropertyType Type { get; set; }
        public uint Area { get; set; }
        public ushort Age { get; set; }
        public bool HasMedia { get; set; }
        public byte Bedroom { get; set; }
        public IList<PostAmenity> PostAmenities { get; set; }
        public uint View { get; set; }
        public double Deposit { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [ForeignKey("CreatorUserId")]
        public User User { get; set; }
        public Post()
        {
            CreationTime = DateTime.Now;
        }
    }
}
 