using COLSTRAT.Domain.Menu.Entity.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace COLSTRAT.Backend.Models
{
    [NotMapped]
    public class GeneralItemView : GeneralItem
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}