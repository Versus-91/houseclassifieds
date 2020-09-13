using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using classifieds.Posts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Categories
{
    public class Category : Entity, IHasCreationTime
    {
        public string Name { get; set; }
        public DateTime CreationTime { get ; set; }
        [JsonIgnore]
        public List<Post> Posts { get; set; }
        public Category()
        {
            CreationTime = DateTime.Now;
        }
    }
}
