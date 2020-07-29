using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Cities.Dto
{
    [AutoMapFrom(typeof(City))]
    public class CityDto:EntityDto
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
