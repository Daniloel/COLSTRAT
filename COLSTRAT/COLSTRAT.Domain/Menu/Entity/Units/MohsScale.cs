﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COLSTRAT.Domain
{
    public class MohsScale
    {
        [Key]
        public int MohsScaleId { get; set; }
        [Required(ErrorMessage = "The field {0} is required.")]
        [Index("MohsScale_Scale_Index", IsUnique = true)]
        [Display(Name = "Mohs Scale")]
        public int Scale { get; set; }

        public string Mineral { get; set; }
        public string Test { get; set; }
    }
}
