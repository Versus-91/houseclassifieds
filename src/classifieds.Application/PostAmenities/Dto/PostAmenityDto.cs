using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Amenities.Dto;
using classifieds.PostsAmenities;

namespace classifieds.PostsAmenities.Dto
{
    [AutoMap(typeof(PostAmenity))]
    public class PostAmenityDto:EntityDto
    {
        public int PostId { get; set; }
        public int AmenityId { get; set; }
        public AmenityDto Amenity { get; set; }

    }
}
