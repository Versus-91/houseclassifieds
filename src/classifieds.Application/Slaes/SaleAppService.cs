using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using classifieds.Authorization;
using classifieds.SaleReports;
using classifieds.SaleReports.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace classifieds.Slaes
{
    [AbpAuthorize(PermissionNames.Pages_Sales)]
    public class SaleAppService : AsyncCrudAppService<SaleReport, SaleReportDto, int>, ISaleAppService
    {
        public SaleAppService(IRepository<SaleReport> repository) : base(repository)
        {

        }
        protected override IQueryable<SaleReport> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return base.CreateFilteredQuery(input).Include(m=>m.Post).Include(m=>m.Category);
        }
    }
}
