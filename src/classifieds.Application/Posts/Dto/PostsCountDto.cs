using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    public class PostsCountDto
    {
        public int Count { get; set; }
        public int CityId{ get; set; }
        public string CityImage { get; set; }
        public string CityName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }


    }
}
