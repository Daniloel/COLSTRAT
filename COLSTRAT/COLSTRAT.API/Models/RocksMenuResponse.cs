using COLSTRAT.Domain.Menu.Entity.Geology.Rocks;
using System.Collections.Generic;

namespace COLSTRAT.API.Models
{
    public class RocksMenuResponse : RocksMenu
    {
        public List<RockResponse> Rocks { get; set; }
    }
}