using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Provinces
{
    public class Province:Entity,IHasCreationTime
    {
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public Province()
        {
            CreationTime = DateTime.Now;
        }
    }
}
