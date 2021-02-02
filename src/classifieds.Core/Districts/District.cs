using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Cities;
using classifieds.Posts;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using classifieds.Areas;

namespace classifieds.Districts
{
    public class District : AuditedEntity, IHasCreationTime
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int? AreaId { get; set; }
        public Area Area { get; set; }
        public District()
        {
            CreationTime = DateTime.Now;
        }
        [JsonIgnore]
        public List<Post> Posts { get; set; }
    }
}
