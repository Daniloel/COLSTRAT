namespace COLSTRAT.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("Category", Schema = "public")]
    public class Category
    {
        //[Key]
        public int CategoryId { get; set; }

       /* [Required(ErrorMessage = "The field {0} is required.")]
        [MaxLength(50,ErrorMessage ="The field {0} only can contain {1} characters lenth")]
        [Index("Category_Description_Index", IsUnique = true)]*/
        public string Description { get; set; }
        
    }
}
