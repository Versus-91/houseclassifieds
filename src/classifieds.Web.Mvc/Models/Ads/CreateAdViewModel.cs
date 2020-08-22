using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace classifieds.Web.Models.Ads
{
    public class CreateAdViewModel
    {
        public AdsViewModel Ad { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Districts { get; set; }
        public SelectList PropertyTypes { get; set; }
    }
}
