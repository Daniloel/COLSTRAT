namespace COLSTRAT.Domain.Menu.Categories
{
    using COLSTRAT.Domain.Menu.Entity.Fluids;
    using COLSTRAT.Domain.Menu.Entity.Generic;
    using COLSTRAT.Domain.Menu.Entity.Geology;
    using COLSTRAT.Domain.Menu.Main;
    using Newtonsoft.Json;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Category
    {
        public int MainMenuId { get; set; }
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(20)]
        [Index(IsUnique = true)]
        [Display(Name = "Category")]
        public string Name { get; set; }
        
        [MaxLength(500, ErrorMessage = "The field {0} only can contain {1} characters lenth")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Icon { get; set; }
        [JsonIgnore]
        public virtual MainMenu MainMenu { get; set; }
        [JsonIgnore]
        public virtual Collection<GeologyCategory> GeologyCategory { get; set; }
        [JsonIgnore]
        public virtual Collection<FluidsCategory> FluidsCategory { get; set; }
        [JsonIgnore]
        public virtual Collection<GeneralItem> GeneralItem { get; set; }
    }
}
