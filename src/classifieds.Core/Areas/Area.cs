using Abp.Domain.Entities;
using classifieds.Cities;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Areas
{
    public class Area:Entity
    {
        public string Image { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
