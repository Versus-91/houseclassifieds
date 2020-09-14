using Abp.AutoMapper;
using classifieds.Cities;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(City))]
    public class CityViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
