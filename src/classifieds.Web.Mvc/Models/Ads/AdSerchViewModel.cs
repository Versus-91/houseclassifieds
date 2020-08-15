using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Models.Ads
{
    public class AdSerchViewModel
    {
        public int Category { get; set; }
        public int Type { get; set; }
        public int City { get; set; }
        public int District { get; set; }
    }
}
