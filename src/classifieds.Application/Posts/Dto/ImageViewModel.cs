using Abp.AutoMapper;
using classifieds.Images;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Image))]
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
    }
}
