using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.Reports.Dto;

namespace classifieds.Reports
{
    public class ReportAppService : AsyncCrudAppService<Report, ReportDto, int, GetReportsInput, CreateReportInput, ReportDto>
    {
        public ReportAppService(IRepository<Report> repository):base(repository)
        {

        }
    }
}
