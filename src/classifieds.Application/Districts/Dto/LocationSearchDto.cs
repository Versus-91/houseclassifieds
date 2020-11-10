using System;
using System.Collections.Generic;
using System.Text;

namespace classifieds.Districts.Dto
{
    public class LocationSearchDto
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public bool IsCity { get; set; }
    }
}
