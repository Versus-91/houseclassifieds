using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Districts.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Districts
{
    public class DistrictAppService:AsyncCrudAppService<District,DistrictDto>, IDistrictAppService
    {
        public DistrictAppService(IRepository<District> repository):base(repository)
        {

        }
    }
}
