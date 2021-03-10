using classifieds.Posts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace classifieds.Web.Views.Shared.Components.Footer
{
    public class FooterViewComponent : classifiedsViewComponent
    {
        private readonly IPostAppService _postService;
        public FooterViewComponent(IPostAppService postService)
        {
            _postService = postService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _postService.PopularPosts();
            return View(model);
        }
    }
}
