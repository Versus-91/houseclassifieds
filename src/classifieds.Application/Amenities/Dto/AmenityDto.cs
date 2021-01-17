using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Amenities.Dto
{
    [AutoMap(typeof(Amenity))]
    public class AmenityDto:EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public IFormFile File { get; set; }
    }
}
