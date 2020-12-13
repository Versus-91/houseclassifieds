using classifieds.Categories.Dto;
using classifieds.Cities.Dto;
using classifieds.Posts.Dto;
using classifieds.PropertyTypes.Dto;
using System.Collections.Generic;

namespace classifieds.Web.Models.Ads
{
    public class AdsIndexViewModel
    {
        public IList<CityDto> Cities { get; set; }
        public IList<LlocationPostsCount> PostsCount { get; set; }
        public IList<UserPostsCountDto> UserPostsCount { get; set; }

        public IReadOnlyList<CategoryDto> Categories { get; set; }
        public IList<PropertyTypeDto> Types { get; set; }
    }
}
