using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Validation;
using classifieds.Authorization;
using classifieds.SaleReports;
using classifieds.SaleReports.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classifieds.Slaes
{
    [AbpAuthorize(PermissionNames.Pages_Sales)]
    public class SaleAppService : AsyncCrudAppService<SaleReport, SaleReportDto, int>, ISaleAppService
    {
        private readonly IRepository<SaleReport> _repository;
        public SaleAppService(IRepository<SaleReport> repository) : base(repository)
        {
            _repository = repository;
        }
        protected override IQueryable<SaleReport> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
        {
            return base.CreateFilteredQuery(input).Include(m=>m.Post).Include(m=>m.Category);
        }
        public  override async Task<SaleReportDto> CreateAsync(SaleReportDto input)
        {
            var sale = _repository.GetAll().Where(m => m.PostId == input.PostId).FirstOrDefaultAsync();
            if (sale != null)
            {
                throw new AbpValidationException("there is a sale report for this post");
            }
            return await base.CreateAsync(input);
        }
    }
}
