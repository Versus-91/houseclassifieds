using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using classifieds.Categories;
using classifieds.Cities;
using classifieds.Districts;
using classifieds.Images;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Post))]
    public class PostDto : EntityDto, IHasCreationTime
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsFeatured { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ushort Area { get; set; }
        public ushort Age { get; set; }
        public byte Bedroom { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public int DistrictId { get; set; }
        public double Deopsit { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public int TypeId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreationTime { get; set; }
        public IList<Image> Images { get; set; }
    }
}
