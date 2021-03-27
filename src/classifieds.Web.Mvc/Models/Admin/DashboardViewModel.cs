using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Models.Admin
{
    public class DashboardViewModel
    {
        public int ProcessorUsage { get; set; }
        public float MemUsage { get; set; }
        public int PostsTotalNumber { get; set; }
        public int UsersTotalNumber { get; set; }
        public int SalesTotal { get; set; }


    }
}
