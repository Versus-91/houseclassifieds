using classifieds.Posts.Dto;
using classifieds.RealEstates.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Models.RealEstate
{
    public class IndexViewMode
    {
        public RealEstateDto RealEstate { get; set; }
        public IReadOnlyList<PostDto> Posts { get; set; }
        public int Page { get; set; }
        public bool HasPassword { get; set; }
        public bool HasNextPage { get; set; }
        public int Total { get; set; }


    }
}
