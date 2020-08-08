using classifieds.Categories.Dto;
using classifieds.Cities.Dto;
using classifieds.PropertyTypes.Dto;
using System.Collections.Generic;

namespace classifieds.Web.Models.Ads
{
    public class AdsIndexViewModel
    {
        public IList<CityDto> Cities { get; set; }
        public IList<CategoryDto> Categories { get; set; }
        public IList<PropertyTypeDto> Types { get; set; }
    }
}
