using COLSTRAT.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace COLSTRAT.ViewModels.Rocks
{
    public class RocksViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        List<Rock> rocks;
        ObservableCollection<Rock> _rocks;
        #endregion

        #region Properties
        public ObservableCollection<Rock> Rocks
        {
            get { return _rocks; }
            set
            {
                if (_rocks != value)
                {
                    _rocks = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rocks)));
                }
            }
        }
        #endregion

        #region Constructor
        public RocksViewModel(List<Rock> rocks)
        {
            this.rocks = rocks;
            Rocks = new ObservableCollection<Rock>(rocks.OrderBy(p => p.Name));
        }
        #endregion
    }
}
