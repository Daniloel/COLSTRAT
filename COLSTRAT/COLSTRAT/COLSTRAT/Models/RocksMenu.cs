namespace COLSTRAT.Models
{
    using System.Collections.Generic;
    public class RocksMenu
    {
        public int RocksMenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Rock> Rocks { get; set; }
    }
}