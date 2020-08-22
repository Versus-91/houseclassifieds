using Abp.Domain.Entities;
using classifieds.PostsAmenities;
using System.Collections.Generic;

namespace classifieds.Amenities
{
    public class Amenity:Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public IList<PostAmenity> PostAmenities { get; set; }
    }
}
