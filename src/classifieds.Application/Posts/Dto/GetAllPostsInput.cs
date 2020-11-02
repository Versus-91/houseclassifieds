using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace classifieds.Posts.Dto
{
    public class GetAllPostsInput : PagedAndSortedResultRequestDto
    {
        public bool? Featured { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinArea { get; set; }
        public int? MaxArea { get; set; }
        public int? District { get; set; }
        public int? City { get; set; }
        public int? Category { get; set; }
        public IList<int> Type { get; set; } = new List<int>();
        public IList<int> Amenities { get; set; } = new List<int>();
        public int? Age { get; set; }
        public int? Beds { get; set; }
    }
}
