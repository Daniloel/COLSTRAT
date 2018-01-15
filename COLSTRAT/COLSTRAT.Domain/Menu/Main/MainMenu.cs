namespace COLSTRAT.Domain.Menu.Main
{
    using COLSTRAT.Domain.Menu.Categories;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class MainMenu
    {
        [Key]
        public int MainMenuId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50,ErrorMessage ="The field {0} only can contain {1} characters lenth")]
        [Index("Category_Description_Index", IsUnique = true)]
        [Display(Name = "Category")]
        public string Description { get; set; }
        [Display(Name = "Icon")]
        public string Icon { get; set; }
        [JsonIgnore]
        public virtual ICollection<Category> Category { get; set; }
    }
}
