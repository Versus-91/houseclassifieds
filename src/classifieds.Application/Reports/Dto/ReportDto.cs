using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Posts.Dto;
using classifieds.ReportOptions.Dto;

namespace classifieds.Reports.Dto
{
    [AutoMap(typeof(Report))]
    public class ReportDto:EntityDto
    {
        public string Description { get; set; }
        public int PostId { get; set; }
        public PostDto Post { get; set; }

        public ReportOptionDto ReportOption { get; set; }
    }
}
