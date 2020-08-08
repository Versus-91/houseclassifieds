using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace classifieds.PropertyTypes.Dto
{
    [AutoMap(typeof(PropertyType))]
    public class PropertyTypeDto: AuditedEntityDto
    {
        public string Name { get; set; }
    }
}
