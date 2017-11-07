namespace COLSTRAT.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rock
    {
        [Key]
        public int RockId { get; set; }
        public int CategoryId { get; set; }
        public int TypeOfRockId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        public string Image { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(140, ErrorMessage = "The field {0} only can contain {1} characters lenth")]
        [Index("Rocks_Name_Index", IsUnique = true)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
        public string Minerals_Composition { get; set; }
        [Display(Name = "This rock is used for?")]
        public string UseFor { get; set; }
        [Display(Name = "Structure or texture of this rock")]
        public string Structure { get; set; }
        [Display(Name = "Chemical composition of this rock")]
        public string Chemical_Composition { get; set; }
        [Display(Name = "Mechanical strength of this rock")]
        public string Mechanical_Strength { get; set; } //TODO propiedades mecanicas debe ser una clase
        [Display(Name = "Porosity of this rock")]
        public string Porosity { get; set; } //TODO debe ser una clase
        [Display(Name = "Hardness mohs of this rock")]
        public int MohsScaleId { get; set; }

        [JsonIgnore]
        public virtual TypeOfRock TypeOfRock { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual MohsScale MohsScale { get; set; }
    }
}
