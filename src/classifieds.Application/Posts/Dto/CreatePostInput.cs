using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Post))]
    public  class CreatePostInput
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [Required]
        public int TypeId { get; set; }
        public ushort Area { get; set; }
        public ushort Age { get; set; }
        public byte Bedroom { get; set; }
        public uint View { get; set; }
        public double Deopsit { get; set; }
        public double Rent { get; set; }
        public double Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
