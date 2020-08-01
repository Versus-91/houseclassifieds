using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Cities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Cities
{
    public class CitiesAppService:AsyncCrudAppService<City, CityDto>, ICitiesAppService
    {
        public CitiesAppService(IRepository<City> repository):base(repository)
        {

        }
    }
}
