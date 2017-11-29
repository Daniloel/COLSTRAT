using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COLSTRAT.API.Models
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public const int GEOLOGY = 1;
        public const int FLUIDS = 2;


    }
}