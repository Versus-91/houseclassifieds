using Abp.Application.Services;
using classifieds.SaleReports;
using classifieds.SaleReports.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Slaes
{
    public interface ISaleAppService: IAsyncCrudAppService<SaleReportDto>
    {
    }
}
