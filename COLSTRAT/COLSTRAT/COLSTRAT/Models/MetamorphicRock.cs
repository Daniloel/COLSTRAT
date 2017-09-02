using SQLite.Net.Attributes;

namespace COLSTRAT.Models
{
    public class MetamorphicRock
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public string Minerals { get; set; }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
