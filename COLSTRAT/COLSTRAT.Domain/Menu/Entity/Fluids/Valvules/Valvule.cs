namespace COLSTRAT.Domain.Menu.Entity.Fluids.Valvules
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Valvule
    {
        [Key]
        public int ValvuleId { get; set; }
        public int FluidsCategoryId { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(140, ErrorMessage = "The field {0} only can contain {1} characters lenth")]
        [Index("Valvules_Name_Index", IsUnique = true)]
        public string Name { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
        [Display(Name = "This valvule is used for?")]
        public string UseFor { get; set; }

        [JsonIgnore]
        public virtual FluidsCategory FluidsCategory { get; set; }
    }
}
