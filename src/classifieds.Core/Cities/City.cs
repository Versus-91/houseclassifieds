﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Areas;
using classifieds.Districts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Cities
{
    public class City: AuditedEntity, IHasCreationTime
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public City()
        {
            CreationTime = DateTime.Now;
        }
        [JsonIgnore]
        public List<District> Districts { get; set; } 
    }
}
