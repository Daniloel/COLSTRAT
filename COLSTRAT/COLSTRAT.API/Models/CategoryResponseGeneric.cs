using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COLSTRAT.API.Models
{
    public class CategoryResponseGeneric<T>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<T> Items { get; set; }
    }
}