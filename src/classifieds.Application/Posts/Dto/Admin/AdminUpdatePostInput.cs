using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Images;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Posts.Admin.Dto
{
    [AutoMap(typeof(Post))]
    public class AdminUpdatePostInput:IEntityDto
    {
        [Required]
        public int Id { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsVerified { get; set; }
    }
}
