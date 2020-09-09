using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
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
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public ushort Area { get; set; }
        public byte Bedroom { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public int TypeId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime CreationTime { get; set; }
        public IList<Image> Images { get; set; }
    }
}
