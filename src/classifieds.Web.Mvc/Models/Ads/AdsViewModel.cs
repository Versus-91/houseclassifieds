using classifieds.Posts.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public IList<Microsoft.AspNetCore.Http.IFormFile> Files { get; set; }
        public uint Price { get; set; }
        public uint Deposit { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public PostDto ToPost()
        {
            return new PostDto
            {
                CategoryId = CategoryId,
                Title = this.Title,
                Description = this.Description,
                DistrictId = this.DistrictId,
                CreationTime = this.CreationTime,
                Area = this.Area,
                Bedroom = this.Bedroom,
                TypeId = this.TypeId,
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
