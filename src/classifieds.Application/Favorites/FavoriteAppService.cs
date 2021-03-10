using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using classifieds.Favorites.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Favorites
{
    [AbpAuthorize]
    public class FavoriteAppService : AsyncCrudAppService<Favorite, FavoriteDto, int, FavoritesInput>, IFavoriteAppService
    {
        private readonly IRepository<Favorite> _repository;
        public FavoriteAppService(IRepository<Favorite> repository):base(repository)
        {
            _repository = repository;
        }
        protected override IQueryable<Favorite> CreateFilteredQuery(FavoritesInput input)
        {
            return base.CreateFilteredQuery(input)
                .Include(m => m.Post)
                .Include(m => m.Post.Category)
                .Include(m => m.Post.Images)
                .Include(m => m.Post.Type)
                .Include(m => m.Post.District.City)
                .Include(m => m.Post.District)
                .OrderByDescending(m => m.CreationTime);

        }
        public override async Task<FavoriteDto> CreateAsync(FavoriteDto input)
        {
            var items = await _repository.GetAllListAsync(m => m.PostId == input.PostId && m.CreatorUserId == AbpSession.UserId);
            if (items == null || items.Count == 0)
            {
                return await base.CreateAsync(input);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        [AbpAllowAnonymous]
        public async Task<FavoriteResult> GetFavorite(int postId)
        {
            var item = await _repository.GetAll()
                .FirstOrDefaultAsync(m => m.PostId == postId && m.CreatorUserId == AbpSession.UserId);
            if (item == null )
            {
                return new FavoriteResult { IsLiked = false,Id = null };
            }
            else
            {
               return new FavoriteResult { IsLiked = true, Id = item.Id };
            }
        }
    }
}
