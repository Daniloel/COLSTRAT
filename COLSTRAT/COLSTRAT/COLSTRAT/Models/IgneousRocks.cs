using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLSTRAT.Models
{
    /// <summary>
    /// Retorna una roca ignea
    /// </summary>
    public class IgneousRock
    {
        public object Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public string Minerals { get; set; }
    }
}
