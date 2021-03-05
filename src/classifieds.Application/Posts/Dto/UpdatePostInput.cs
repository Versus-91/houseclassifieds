using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Images;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Post))]
    public class UpdatePostInput:IEntityDto
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public ushort Area { get; set; }
        public ushort Age { get; set; }
        public byte Bedroom { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int DistrictId { get; set; }
        public double Deopsit { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public int TypeId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IList<int> Amenities { get; set; } = new List<int>();

    }
}
