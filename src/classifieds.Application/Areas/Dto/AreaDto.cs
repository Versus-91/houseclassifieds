﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Cities;
using classifieds.Cities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Areas.Dto
{
    [AutoMap(typeof(Area))]
    public class AreaDto:EntityDto
    {
        public int CityId { get; set; }
        public CityDto City { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
