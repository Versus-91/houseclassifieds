using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.RealEstates.Dto
{
    public class GetRealEstatesInput
    {
        public int? Id { get; set; }
        public int Name { get; set; }
        public int Owner { get; set; }

    }
}
