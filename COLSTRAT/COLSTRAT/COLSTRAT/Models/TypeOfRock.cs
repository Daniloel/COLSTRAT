using System.Collections.Generic;

namespace COLSTRAT.Models
{
    public class TypeOfRock : Category
    {
        public int TypeOfRockId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Rock> Rocks { get; set; }
    }
}
