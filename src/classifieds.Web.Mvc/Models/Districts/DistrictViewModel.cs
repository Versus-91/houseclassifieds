using classifieds.Areas.Dto;
using classifieds.Cities.Dto;
using classifieds.Districts.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace classifieds.Web.Models.Districts
{
    public class DistrictViewModel
    {
        public classifieds.Districts.Dto.DistrictDto District { get; set; }
        public List<CityDto> Cities { get; set; }
        public List<AreaDto> Areas { get; set; }


    }
}
