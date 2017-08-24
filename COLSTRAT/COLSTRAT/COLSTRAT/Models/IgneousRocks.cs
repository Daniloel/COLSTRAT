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
        public object Id
        {
            get { return Id; }
            set { Id = value; }
        }
        public string Image
        {
            get { return Image; }
            set { Image = value; }
        }
        public string Name
        {
            get { return Name; }
            set { Name = value; }
        }
        public string Descripcion
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }
        public string Minerals
        {
            get { return Minerals; }
            set { Minerals = value; }
        }


    }
}
