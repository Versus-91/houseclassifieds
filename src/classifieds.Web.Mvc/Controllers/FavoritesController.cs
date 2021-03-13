using Abp.Application.Services.Dto;
using Abp.Authorization;
using classifieds.Controllers;
using classifieds.Favorites;
using classifieds.Favorites.Dto;
using classifieds.Posts.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace classifieds.Web.Controllers
{
    [AbpAuthorize]
    public class FavoritesController : classifiedsControllerBase
    {
        private readonly IFavoriteAppService _favoritesService;

        public FavoritesController(IFavoriteAppService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        public async Task<IActionResult> Index()
        {
            var items = (await _favoritesService.GetAllAsync(new FavoritesInput(){MaxResultCount=100,UserId=AbpSession.UserId.Value })).Items;
            return View(items);
        }
        public async Task<IActionResult> Delete(int id)
        {
             await _favoritesService.DeleteAsync(new FavoriteDto { Id = id });
            return RedirectToAction("Index");
        }
    }
}
