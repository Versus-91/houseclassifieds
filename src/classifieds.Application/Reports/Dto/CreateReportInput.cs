using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace classifieds.Reports.Dto
{
    [AutoMap(typeof(Report))]
    public class CreateReportInput 
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int ReportOptionId { get; set; }
        public string Description { get; set; }
    }
}
