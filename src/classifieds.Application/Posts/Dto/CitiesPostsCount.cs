using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    public class CitiesPostsCount
    {
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public string CityName { get; set; }
        public int CityId { get; set; }
        public List<PostsCountDto> Count { get; set; } = new List<PostsCountDto>();
    }
}
