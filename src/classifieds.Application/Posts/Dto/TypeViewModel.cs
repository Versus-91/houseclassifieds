using Abp.AutoMapper;
using classifieds.PropertyTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(PropertyType))]
    public class TypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
