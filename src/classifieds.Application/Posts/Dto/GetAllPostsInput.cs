using Abp.Application.Services.Dto;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace classifieds.Posts.Dto
{
    public class GetAllPostsInput : PagedAndSortedResultRequestDto
    {
        public long? UserId { get; set; }
        public bool? Featured { get; set; }
        public int? Zone { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? RealEstateId { get; set; }

        public int? MinArea { get; set; }
        public int? MaxArea { get; set; }
        public int? District { get; set; }
        public int? City { get; set; }
        public int? Category { get; set; }
        public List<int> Types { get; set; }
        public List<int> Amenities { get; set; }
        public int? Age { get; set; }
        public long? Id { get; set; }

        public bool? HasMedia { get; set; }

        public int? Beds { get; set; }
        [Range(1,100)]
        public override int MaxResultCount { get => base.MaxResultCount; set => base.MaxResultCount = value; }
    }
}
