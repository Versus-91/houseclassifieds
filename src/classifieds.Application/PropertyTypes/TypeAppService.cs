using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.PropertyTypes.Dto;

namespace classifieds.PropertyTypes
{
    public class TypeAppService: AsyncCrudAppService<PropertyType,PropertyTypeDto>, ITypeAppService
    {
        public TypeAppService(IRepository<PropertyType> repository):base(repository)
        {

        }
    }
}
