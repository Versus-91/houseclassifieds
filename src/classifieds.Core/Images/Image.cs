using Abp.Domain.Entities;
using classifieds.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Images
{
    public class Image:Entity
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
