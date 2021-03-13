using Abp.Domain.Entities;
using classifieds.Posts;
using classifieds.ReportOptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Reports
{
    public class Report : Entity
    {
        public string Description { get; set; }
        public string IpAddress { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }
        public int ReportOptionId { get; set; }
        public ReportOption ReportOption { get; set; }

    }
}
