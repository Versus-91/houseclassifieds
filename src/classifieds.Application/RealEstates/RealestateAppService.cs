using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using classifieds.Authorization;
using classifieds.RealEstates.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.RealEstates
{
    [AbpAuthorize(PermissionNames.Pages_RealEstates)]
    public class RealestateAppService : AsyncCrudAppService<RealEstate, RealEstateDto, int>
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _path;
        private IRepository<RealEstate> _repository;
        public RealestateAppService(IRepository<RealEstate> repository, IWebHostEnvironment env, IConfiguration config) : base(repository)
        {
            _repository = repository;
            _path = config.GetValue<string>("IconsFilesPath");
            _env = env;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var item = await _repository.FirstOrDefaultAsync(m => m.Id == input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            if (string.IsNullOrWhiteSpace(item.Logo))
            {
                await _repository.DeleteAsync(item);
            }
            else
            {
                File.Delete(Path.Combine(_env.WebRootPath, item.Logo));
                await _repository.DeleteAsync(item);
            }
        }
        [HttpGet]
        public async Task<List<RealEstateDto>> Find(string term)
        {
            var items = await _repository.GetAll().Where(m => m.Name.Contains(term) || m.Owner.Contains(term)).ToListAsync();
            return ObjectMapper.Map<List<RealEstateDto>>(items) ;
        }
    }
}
