using Abp.Application.Services;
using Abp.Domain.Repositories;
using classifieds.ReportOptions.Dto;

namespace classifieds.ReportOptions
{
    public class ReportOptionAppService : AsyncCrudAppService<ReportOption, ReportOptionDto, int>
    {
        public ReportOptionAppService(IRepository<ReportOption> repository) :base(repository)
        {

        }
    }
 
}
