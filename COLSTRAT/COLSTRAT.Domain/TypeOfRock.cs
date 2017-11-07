using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COLSTRAT.Domain
{
    public class TypeOfRock
    {
        [Key]
        public int TypeOfRockId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        [Display(Name = "Type")]
        public string Name { get; set; }
        
        [MaxLength(500, ErrorMessage = "The field {0} only can contain {1} characters lenth")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public virtual Collection<Rock> Rocks { get; set; }
    }
}
