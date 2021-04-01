using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.Cities.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace classifieds.Cities
{
    public class CityAppService : AsyncCrudAppService<City, CityDto>, ICityAppService
    {
        private readonly IRepository<City> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly string _path;
        public CityAppService(IRepository<City> repository, IWebHostEnvironment env, IConfiguration config) : base(repository)
        {
            _repository = repository;
            _path = config.GetValue<string>("IconsFilesPath");
            _env = env;
            CreatePermissionName = PermissionNames.Pages_Cities;
            UpdatePermissionName = PermissionNames.Pages_Cities;
            DeletePermissionName = PermissionNames.Pages_Cities;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var item = await _repository.FirstOrDefaultAsync(m => m.Id == input.Id);
            if (item == null)
            {
                throw new Exception();
            }
            if (string.IsNullOrWhiteSpace(item.Image))
            {
                await _repository.DeleteAsync(item);
            }
            else
            {
                File.Delete(Path.Combine(_env.WebRootPath, item.Image));
                await _repository.DeleteAsync(item);
            }
        }
        public override async Task<CityDto> CreateAsync(CityDto input)
        {
            var city = new City();
            city.Name = input.Name;
            if (input.File != null)
            {
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(input.File.FileName);
                var trustedFileNameForFileStorage = Path.Combine($"{Guid.NewGuid().ToString("N")}{Path.GetExtension(trustedFileNameForDisplay).ToLower()}");
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _path));
                using (var stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, _path, trustedFileNameForFileStorage)))
                {
                    await input.File.CopyToAsync(stream);
                }
                city.Image = Path.Combine(_path, trustedFileNameForFileStorage);
            }
            await _repository.InsertAndGetIdAsync(city);
            return ObjectMapper.Map<CityDto>(city);
        }
        public override async Task<CityDto> UpdateAsync(CityDto input)
        {
            var city = await _repository.GetAsync(input.Id);
            if (city == null)
            {
                throw new Exception();
            }
            city.Name = input.Name;
            if (input.File != null)
            {
                if (!string.IsNullOrEmpty(input.Image))
                {
                    File.Delete(Path.Combine(_env.WebRootPath, input.Image));
                }
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(input.File.FileName);
                var trustedFileNameForFileStorage = Path.Combine($"{Guid.NewGuid().ToString("N")}{Path.GetExtension(trustedFileNameForDisplay).ToLower()}");
                Directory.CreateDirectory(Path.Combine(_env.WebRootPath, _path));
                using (var stream = System.IO.File.Create(Path.Combine(_env.WebRootPath, _path, trustedFileNameForFileStorage)))
                {
                    await input.File.CopyToAsync(stream);
                }
                city.Image = Path.Combine(_path, trustedFileNameForFileStorage);
            }
            await _repository.UpdateAsync(city);
            return ObjectMapper.Map<CityDto>(city);
        }
    }
}
