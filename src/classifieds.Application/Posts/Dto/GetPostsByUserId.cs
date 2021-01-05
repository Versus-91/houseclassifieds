using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    public class GetPostsByUserId : PagedAndSortedResultRequestDto
    {
        public int UserId { get; set; }
    }
}
