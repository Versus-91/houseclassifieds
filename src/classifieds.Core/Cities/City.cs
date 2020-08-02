using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Cities
{
    public class City: AuditedEntity, IHasCreationTime
    {
        public string Name { get; set; }
        public City()
        {
            CreationTime = DateTime.Now;
        }
        public List<District> Districts { get; set; } 
    }
}
