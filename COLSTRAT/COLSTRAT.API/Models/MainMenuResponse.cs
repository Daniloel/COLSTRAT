using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COLSTRAT.API.Models
{
    public class MainMenuResponse
    {
        public int MainMenuId { get; set; }
        
        public string Description { get; set; }
        
        public List<CategoryResponse> Category { get; set; }
    }
}