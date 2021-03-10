using Abp.Domain.Entities;
using classifieds.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.ReportOptions
{
    public class ReportOption:Entity
    {
        public string Name { get; set; }
        public bool HasDescription { get; set; }

    }
}
