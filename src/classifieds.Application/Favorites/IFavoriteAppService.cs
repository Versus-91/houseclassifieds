using Abp.Application.Services;
using classifieds.Favorites.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Favorites
{
    public interface IFavoriteAppService : IAsyncCrudAppService<FavoriteDto, int, FavoritesInput>
    {
        Task<FavoriteResult> GetFavorite(int postId);
    }
}
