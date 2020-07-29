using classifieds.Categories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Models.Categories
{
    public class CategoryListViewModel
    {
        public IReadOnlyList<CategoryDto> Categories { get; set; }
    }
}
