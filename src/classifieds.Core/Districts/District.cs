using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Districts
{
    public class District : AuditedEntity, IHasCreationTime
    {
        public string Name { get; set; }
        public District()
        {
            CreationTime = DateTime.Now;
        }
    }
}
