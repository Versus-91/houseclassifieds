using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Areas;
using classifieds.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Districts.Dto
{
    [AutoMap(typeof(District))]
    public class DistrictDto:AuditedEntityDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
        [Range(1,int.MaxValue)]
        public int? AreaId { get; set; }
        public Area Area { get; set; }

    }
}
