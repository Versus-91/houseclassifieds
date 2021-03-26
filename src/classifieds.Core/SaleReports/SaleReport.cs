using Abp.Domain.Entities;
using classifieds.Categories;
using classifieds.Posts;
using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.SaleReports
{
    public class SaleReport:Entity
    {
        public double Price { get; set; }
        public int PostId { get; set; }
        public string Benefits { get; set; }

        public DateTime SaleDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Post Post { get; set; }

    }
}
