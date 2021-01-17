using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.ReportOptions.Dto
{
    [AutoMap(typeof(ReportOption))]
    public class ReportOptionDto:EntityDto
    {
        public string Name { get; set; }
    }
}
