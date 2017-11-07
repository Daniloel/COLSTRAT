namespace COLSTRAT.Domain
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50,ErrorMessage ="The field {0} only can contain {1} characters lenth")]
        [Index("Category_Description_Index", IsUnique = true)]
        public string Description { get; set; }
        
        public virtual ICollection<Rock> Rocks { get; set; }
        public virtual ICollection<MohsScale> MohsScales { get; set; }
    }
}
