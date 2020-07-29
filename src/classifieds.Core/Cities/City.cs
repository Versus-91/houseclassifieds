using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
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
    }
}
