﻿namespace COLSTRAT.Backend.Models
{
    using COLSTRAT.Domain.Menu.Entity.Geology.Rocks;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    [NotMapped]
    public class RockView : Rock
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}