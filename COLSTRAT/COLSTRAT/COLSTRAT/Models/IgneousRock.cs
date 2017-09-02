using SQLite.Net.Attributes;

namespace COLSTRAT.Models
{
    /// <summary>
    /// Retorna una roca ignea
    /// </summary>
    public class IgneousRock
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
