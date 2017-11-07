using SQLite.Net.Attributes;

namespace COLSTRAT.Models
{
    /// <summary>
    /// Retorna una roca ignea
    /// </summary>
    public class IgneousRock
    {
        public int RockId { get; set; }
        public int CategoryId { get; set; }
        public int TypeOfRockId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public string Minerals_Composition { get; set; }
        public object UseFor { get; set; }
        public object Structure { get; set; }
        public object Chemical_Composition { get; set; }
        public object Mechanical_Strength { get; set; }
        public object Porosity { get; set; }
        public int MohsScaleId { get; set; }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
