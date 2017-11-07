using System.ComponentModel.DataAnnotations;

namespace COLSTRAT.Domain
{
    public class MohsScale
    {
        public int CategoryId { get; set; }
        [Key]
        public int MohsScaleId { get; set; }
        public int Scale { get; set; }
        public string Mineral { get; set; }
        public string Test { get; set; }

        public virtual Category Category { get; set; }
    }
}
