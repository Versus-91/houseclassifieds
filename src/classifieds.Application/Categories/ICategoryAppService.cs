using Abp.Application.Services;
using classifieds.Categories.Dto;
using System.Threading.Tasks;

namespace classifieds.Categories
{
    public interface ICategoryAppService : IAsyncCrudAppService<CategoryDto>
    {
    }
}
