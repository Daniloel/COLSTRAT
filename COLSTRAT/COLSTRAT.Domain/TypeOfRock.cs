using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COLSTRAT.Domain
{
    public class TypeOfRock
    {

        public int CategoryId { get; set; }
        [Key]
        public int ArticleId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        [Display(Name = "Type")]
        public string Name { get; set; }
        
        [MaxLength(500, ErrorMessage = "The field {0} only can contain {1} characters lenth")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual Category Category { get; set; }
        [JsonIgnore]
        public virtual Collection<Rock> Rocks { get; set; }
    }
}
