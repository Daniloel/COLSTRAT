namespace COLSTRAT.API.Models
{
    public class RockResponse
    {
        public int RockId { get; set; }
        public int TypeOfRockId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public string Minerals_Composition { get; set; }
        public string UseFor { get; set; }
        public string Structure { get; set; }
        public string Chemical_Composition { get; set; }
        public string Mechanical_Strength { get; set; }
        public string Porosity { get; set; }
        public int MohsScaleId { get; set; }
    }
}