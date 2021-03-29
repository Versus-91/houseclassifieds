using Abp.AutoMapper;
using classifieds.Amenities.Dto;
using classifieds.PostsAmenities.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Post))]
    public  class CreatePostInput
    {

        public string Description { get; set; }
        [Required(ErrorMessage = "دسته بندی را مشخص کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "دسته بندی را انتخاب کنید")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "محل را انتخاب کنید")]
        [Range(1, int.MaxValue, ErrorMessage = "محل را انتخاب کنید")]
        public int DistrictId { get; set; }
        [Required(ErrorMessage = "نوع ملک باید پر شود.")]
        [Range(1, int.MaxValue, ErrorMessage = "نوع ملک را انتخاب کنید")]
        public int TypeId { get; set; }
        [Range(0, ushort.MaxValue, ErrorMessage = "حداکثر مقدار {2} است.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "مساحت را پر کنید")]
        public ushort Area { get; set; }
        [Range(0, byte.MaxValue, ErrorMessage = "حداکثر مقدار {2} است.")]
        public byte? Bedroom { get; set; }
        public double? Deposit { get; set; }
        public double? Rent { get; set; }
        public double? Price { get; set; }
        public double? Latitude { get; set; }
        public ushort? Age { get; set; }
        public int? RealEstateId { get; set; }

        public double? Longitude { get; set; }
        public IList<int> Amenities { get; set; } = new List<int>();

    }
}
