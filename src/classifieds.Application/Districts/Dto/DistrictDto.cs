using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Cities;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Districts.Dto
{
    [AutoMap(typeof(District))]
    public class DistrictDto:AuditedEntityDto
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
    }
}
