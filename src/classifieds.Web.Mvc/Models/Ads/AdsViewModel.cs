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
        public int CategoryId { get; set; }
        public int DistrictId { get; set; }
        public DateTime CreationTime { get; set; }
        public PostDto ToPost() {
            return new PostDto
            {
                CategoryId = this.CategoryId,
                Title = this.Title,
                Description = this.Title,
                DistrictId = this.DistrictId,
                CreationTime = this.CreationTime,

            };
        }
    }
}
