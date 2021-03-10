using Abp.AutoMapper;
using classifieds.Areas;
using classifieds.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(District))]
    public class DistrictViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityViewModel City { get; set; }
    }
}
