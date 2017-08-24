using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLSTRAT.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using System.Windows.Input;

    public class IgneousViewModel
    {
        #region properties
        /// <summary>
        /// La propiedad contiene una coleccion de rocas igneas
        /// </summary>
        /// <return></return>
        public ObservableCollection<IgneousRock> IgneousRocks { get; set; }
        public IgneousRock SourceRock { get; set; }
        public bool IsRunning { get; set; }
        public bool IsEnable { get; set; }
        #endregion

        #region contructor
        public IgneousViewModel()
        {

        }
        #endregion

        #region Commands

        public ICommand ShowCommand
        {
            get
            {
                return new RelayCommand(Show());
            }
        }

        private Action Show()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
