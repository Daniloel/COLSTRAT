using COLSTRAT.Models;
using COLSTRAT.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace COLSTRAT.ViewModels
{
    public class TypesOfRocksViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        ObservableCollection<CategoryTypeOfRocks> _typesOfRocks;
        #endregion

        #region Properties
        public ObservableCollection<CategoryTypeOfRocks> TypeOfRocks
        {
            get { return _typesOfRocks; }
            set
            {
                if (_typesOfRocks != value)
                {
                    _typesOfRocks = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TypeOfRocks)));
                }
            }
        }
        #endregion

        #region Constructor
        public TypesOfRocksViewModel()
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            LoadTypesOfRocks();
        }
        #endregion

        #region Methods
        private async void LoadTypesOfRocks()
        {
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                await dialogService.ShowErrorMessage(con.Message);
                return;
            }
            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            var response = await apiService.GetList<CategoryTypeOfRocks>(
                urlBase,
                "/api",
                "/TypeOfRocks/2",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowErrorMessage(response.Message);
                return;
            }

            var typeofRocks = (List<CategoryTypeOfRocks>)response.Result;
        }
        #endregion



    }
}
