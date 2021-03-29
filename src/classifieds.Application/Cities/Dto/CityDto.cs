using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Cities.Dto
{
    [AutoMap(typeof(City))]
    public class CityDto:AuditedEntityDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }

    }
}
