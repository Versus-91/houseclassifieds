using Abp.Domain.Entities;
using classifieds.Posts;
using Newtonsoft.Json;

namespace classifieds.Images
{
    public class Image : Entity
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public int PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }
    }
}
