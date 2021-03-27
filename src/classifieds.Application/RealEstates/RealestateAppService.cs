using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.RealEstates.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.RealEstates
{
    [AbpAuthorize(PermissionNames.Pages_RealEstates)]
    public class RealestateAppService : AsyncCrudAppService<RealEstate, RealEstateDto, int>
    {
        public RealestateAppService(IRepository<RealEstate> repository):base(repository)
        {
                
        }
    }
}
