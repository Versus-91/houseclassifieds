using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using classifieds.Amenities.Dto;
using classifieds.Categories;
using classifieds.Cities;
using classifieds.Districts;
using classifieds.Images;
using classifieds.PostsAmenities;
using classifieds.PostsAmenities.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Post))]
    public class PostDto : AuditedEntityDto, IHasCreationTime
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int DistrictId { get; set; }
        public int TypeId { get; set; }
        public long UserId { get; set; }

        public bool IsFeatured { get; set; }
        public bool IsVerified { get; set; }
        public CategoryViewModel Category { get; set; }
        public uint Area { get; set; }
        public ushort Age { get; set; }
        public byte Bedroom { get; set; }
        public DistrictViewModel District { get; set; }
        public double Deposit { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public TypeViewModel Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IList<ImageViewModel> Images { get; set; }
        public IList<AmenityDto> Amenities { get; set; }
    }
}
