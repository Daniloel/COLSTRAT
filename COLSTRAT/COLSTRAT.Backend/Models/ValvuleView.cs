namespace COLSTRAT.Backend.Models
{
    using COLSTRAT.Domain.Menu.Entity.Fluids.Valvules;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    [NotMapped]
    public class ValvuleView : Valvule
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}