using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Areas;
using classifieds.Areas.Dto;
using classifieds.Cities;
using classifieds.Cities.Dto;
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
        [Range(1,int.MaxValue)]
        public int? AreaId { get; set; }
        public AreaDto Area { get; set; }

    }
}
