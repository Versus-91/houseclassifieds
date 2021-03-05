using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Favorites.Dto
{
    public class FavoritesInput: PagedAndSortedResultRequestDto
    {
        public long UserId { get; set; }
    }
}
