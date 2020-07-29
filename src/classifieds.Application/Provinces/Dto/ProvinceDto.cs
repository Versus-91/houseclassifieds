using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace classifieds.Provinces.Dto
{
    [AutoMapFrom(typeof(Province))]
    public class ProvinceDto:EntityDto
    {
        [Required]
        public string Name { get; set; }
    }
}
