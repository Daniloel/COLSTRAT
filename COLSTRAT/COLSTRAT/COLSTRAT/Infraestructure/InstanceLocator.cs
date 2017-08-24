using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLSTRAT.Infraestructure
{
    using ViewModels;
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public IgneousViewModel Igneous { get; set; }
        public MetamorphicViewModel Metamorphic { get; set; }
        public SedimentaryViewModel Sedimentary { get; set; }
        public InstanceLocator()
        {

        }

    }
}
