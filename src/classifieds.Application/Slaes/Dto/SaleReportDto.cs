using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using classifieds.Categories.Dto;
using classifieds.Posts.Dto;
using System;
namespace classifieds.SaleReports.Dto
{
    [AutoMap(typeof(SaleReport))]
    public class SaleReportDto:EntityDto
    {
        public double Price { get; set; }
        public int PostId { get; set; }
        public string Benefits { get; set; }
        public DateTime SaleDate { get; set; }
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }
        public PostDto Post { get; set; }
    }


}
