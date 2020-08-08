using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.PropertyTypes
{
    public class PropertyType:AuditedEntity
    {
        public string Name { get; set; }
    }
}
