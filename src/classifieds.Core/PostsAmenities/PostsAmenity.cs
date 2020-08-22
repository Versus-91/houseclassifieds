using Abp.Domain.Entities;
using classifieds.Amenities;
using classifieds.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.PostsAmenities
{
    public class PostAmenity:Entity  
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int AmenityId { get; set; }

        public Amenity Amenity { get; set; }
    }
}
