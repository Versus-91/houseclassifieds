using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.RealEstates.Dto
{
    public class GetRealEstatesInput:PagedAndSortedResultRequestDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string? Owner { get; set; }
        public int? District { get; set; }
        public int? City { get; set; }


    }
}
