using COLSTRAT.Models;
using COLSTRAT.Service;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System;

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
        ObservableCollection<TypeOfRock> _typesOfRocks;
        #endregion

        #region Properties
        public ObservableCollection<TypeOfRock> TypeOfRocks
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
            var response = await apiService.GetList<TypeOfRock>(
                urlBase,
                "/api",
                "/TypeOfRocks",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowErrorMessage(response.Message);
                return;
            }

            var typeofRocks = (List<TypeOfRock>)response.Result;
            TypeOfRocks = new ObservableCollection<TypeOfRock>(typeofRocks.OrderBy(c => c.Description));
        }
        #endregion

        

    }
}
