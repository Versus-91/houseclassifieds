using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using classifieds.Provinces.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Provinces
{
    public class ProvincesAppService : AsyncCrudAppService<Province, ProvinceDto>, IProvincesAppService
    {
        public ProvincesAppService(IRepository<Province> repository):base(repository)
        {

        }
    }
}
