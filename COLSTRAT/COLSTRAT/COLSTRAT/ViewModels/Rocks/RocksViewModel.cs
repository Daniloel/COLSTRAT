using COLSTRAT.Models;
using COLSTRAT.Service;
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
        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Attributes
        List<Rock> rocks;
        ObservableCollection<Rock> _rocks;
        bool _isRefreshing;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
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
        public RocksViewModel(System.Collections.Generic.List<Models.Rock> rocks)
        {
            this.rocks = rocks;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            Rocks = new ObservableCollection<Rock>(rocks.OrderBy(c => c.Name));
        }
        #endregion
    }
}
