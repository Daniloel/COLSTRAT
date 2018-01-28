using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COLSTRAT.API.Models
{
    public class FluidsCategoryResponse
    {
        public int FluidsCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ValvuleResponse> Valvules { get; set; }

    }
}