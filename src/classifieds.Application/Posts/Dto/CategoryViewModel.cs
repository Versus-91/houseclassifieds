using Abp.AutoMapper;
using classifieds.Categories;

namespace classifieds.Posts.Dto
{
    [AutoMap(typeof(Category))]
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
