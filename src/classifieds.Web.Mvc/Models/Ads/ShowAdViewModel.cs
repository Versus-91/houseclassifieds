using classifieds.Posts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Models.Ads
{
    public class ShowAdViewModel
    {
        public PostDto Post { get; set; }
        public IReadOnlyList<PostDto> Recommendations { get; set; }

    }
}
