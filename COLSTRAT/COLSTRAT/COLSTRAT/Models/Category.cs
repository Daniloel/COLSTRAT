using System.Collections.Generic;

namespace COLSTRAT.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<TypeOfRock> TypesOfRocks { get; set; }
    }
}
