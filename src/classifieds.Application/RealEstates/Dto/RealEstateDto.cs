using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Districts.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.RealEstates.Dto
{
    [AutoMap(typeof(RealEstate))]
    public class RealEstateDto : EntityDto
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string PhoneNumbers { get; set; }
        public IFormFile File { get; set; }

        public int DistrictId { get; set; }
        public DistrictDto District { get; set; }
    }
}
