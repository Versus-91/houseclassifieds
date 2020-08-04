using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using classifieds.Cities.Dto;
using classifieds.Districts.Dto;
using Microsoft.AspNetCore.Mvc;

namespace classifieds.Web.Models.Districts
{
    public class DistrictViewModel 
    {
        public DistrictDto District { get; set; }
        public IReadOnlyList<CityDto> Cities{ get; set; }

    }
}
