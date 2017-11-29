using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COLSTRAT.API.Models
{
    public class GeologyCategoryResponse
    {
        public int GeologyCategoryId { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        public List<RocksMenuResponse> RocksMenu { get; set; }

    }
}