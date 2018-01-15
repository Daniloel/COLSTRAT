using COLSTRAT.Domain.Menu.Categories;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COLSTRAT.Domain.Menu.Entity.Generic
{
    public class GeneralItem
    {
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Key]
        public int GeneralItemId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(2000, ErrorMessage = "The field {0} only can contain {1} characters lenth")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
