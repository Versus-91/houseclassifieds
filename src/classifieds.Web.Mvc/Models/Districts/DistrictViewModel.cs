using classifieds.Districts.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace classifieds.Web.Models.Districts
{
    public class DistrictViewModel
    {
        public DistrictDto District { get; set; }
        public SelectList Cities { get; set; }

    }
}
