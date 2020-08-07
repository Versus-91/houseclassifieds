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
        public ushort Area { get; set; }
        public byte Bedroom { get; set; }
        public DateTime CreationTime { get; set; }
        public IList<Microsoft.AspNetCore.Http.IFormFile> Files { get; set; }
        public PostDto ToPost() {
            return new PostDto
            {
                CategoryId = this.CategoryId,
                Title = this.Title,
                Description = this.Title,
                DistrictId = this.DistrictId,
                CreationTime = this.CreationTime,
                Area = this.Area,
                Bedroom = this.Bedroom,

            };
        }
    }
}
