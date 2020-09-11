using classifieds.Posts.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace classifieds.Web.Models.Ads
{
    public class AdsViewModel
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
        public byte Bedroom { get; set; }
        public DateTime CreationTime { get; set; }
        public uint Price { get; set; }
        public uint Deposit { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public CreatePostInput ToPost()
        {
            return new CreatePostInput
            {
                CategoryId = CategoryId,
                Title = Title,
                Description = Description,
                DistrictId = DistrictId,
                Price = Price,
                Deopsit = Deposit,
                Area = Area,
                Bedroom = Bedroom,
                TypeId = TypeId,
                Latitude = Latitude,
                Longitude = Longitude,

            };
        }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //        yield return new ValidationResult("");
        //}
    }
}
