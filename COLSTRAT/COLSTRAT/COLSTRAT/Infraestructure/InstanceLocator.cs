using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLSTRAT.Infraestructure
{
    using GalaSoft.MvvmLight.Ioc;
    using ViewModels;

    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public IgneousViewModel Igneous
        {
            get
            {
                return new IgneousViewModel();
            }
            set
            {
                    Igneous = value;
            }
        }

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }

    }
}
