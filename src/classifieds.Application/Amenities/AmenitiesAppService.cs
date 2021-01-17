using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Amenities.Dto;
using classifieds.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace classifieds.Amenities
{
    public class AmenityAppService: AsyncCrudAppService<Amenity, AmenityDto>
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _path;
        private IRepository<Amenity> _repository;
        public AmenityAppService(IRepository<Amenity> repository, IWebHostEnvironment env, IConfiguration config) :base(repository: repository)
        {
            CreatePermissionName = PermissionNames.Pages_Amenities;
            UpdatePermissionName = PermissionNames.Pages_Amenities;
            DeletePermissionName = PermissionNames.Pages_Amenities;
            _repository = repository;
            _path = config.GetValue<string>("IconsFilesPath");
            _env = env; 
        }

    }
}
