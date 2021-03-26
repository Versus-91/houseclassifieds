using Abp.Domain.Entities.Auditing;
using classifieds.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.RealEstates
{
    public class RealEstate: AuditedEntity
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string PhoneNumbers { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
