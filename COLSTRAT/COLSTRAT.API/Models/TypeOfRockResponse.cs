using System.Collections.Generic;

namespace COLSTRAT.API.Models
{
    public class TypeOfRockResponse
    {
        public int TypeOfRockId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RockResponse> Rocks { get; set; }
    }
}