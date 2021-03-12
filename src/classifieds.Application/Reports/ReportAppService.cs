using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.Reports.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Reports
{
    public class ReportAppService : AsyncCrudAppService<Report, ReportDto, int, GetReportsInput, CreateReportInput, ReportDto>
    {
        private readonly IRepository<Report> _repository;
        public ReportAppService(IRepository<Report> repository):base(repository)
        {
            _repository = repository;
            DeletePermissionName = PermissionNames.Pages_Reports;
            UpdatePermissionName = PermissionNames.Pages_Reports;
            GetAllPermissionName = PermissionNames.Pages_Reports;
            GetPermissionName = PermissionNames.Pages_Reports;
        }
        protected override IQueryable<Report> CreateFilteredQuery(GetReportsInput input)
        {

            return base.CreateFilteredQuery(input)
                .Include(m => m.Post);
        }
        [RemoteService(false)]
        public  async Task<ReportDto> GetReport(int id)
        {
            var report = await _repository
                .GetAllIncluding(m => m.Post, m => m.ReportOption,
                 m => m.Post.Category, m => m.Post.District, 
                 m => m.Post.District.Area,
                 m => m.Post.District.Area.City,
                 m => m.Post.Type)
                .Where(m => m.Id == id).FirstOrDefaultAsync();
            if (report == null)
            {
                throw new ArgumentNullException();
            }
            return MapToEntityDto(report);
        }
    }
}
