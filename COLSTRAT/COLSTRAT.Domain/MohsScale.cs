using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COLSTRAT.Domain
{
    public class MohsScale
    {
        public int CategoryId { get; set; }
        [Key]
        public int MohsScaleId { get; set; }
        [Index("MohsScale_Scale_Index", IsUnique = true)]
        public int Scale { get; set; }
        public string Mineral { get; set; }
        public string Test { get; set; }

        public virtual ICollection<Rock> Rocks { get; set; }
        public virtual Category Category { get; set; }
    }
}
