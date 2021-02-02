using classifieds.Cities.Dto;
using classifieds.Districts.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace classifieds.Web.Models.Districts
{
    public class DistrictViewModel
    {
        public DistrictDto District { get; set; }
        public List<CityDto> Cities { get; set; }

    }
}
