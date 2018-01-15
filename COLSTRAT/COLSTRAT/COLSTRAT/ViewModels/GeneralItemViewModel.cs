using COLSTRAT.Models;
using COLSTRAT.Service;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace COLSTRAT.ViewModels
{
    public class GeneralItemViewModel : INotifyPropertyChanged
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
        bool _isRefreshing;
        List<GeneralItem> generalItems;
        ObservableCollection<GeneralItem> _generalItems;
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
        public ObservableCollection<GeneralItem> GeneralItems
        {
            get { return _generalItems; }
            set
            {
                if (_generalItems != value)
                {
                    _generalItems = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GeneralItems)));
                }
            }
        }
        #endregion

        #region Contructor
        public GeneralItemViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            LoadGeneralItems();
        }
        #endregion

        #region Methods

        private async void LoadGeneralItems()
        {
            IsRefreshing = true;
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowErrorMessage(con.Message);
                return;
            }
            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            var response = await apiService.GetList<GeneralItem>(
                urlBase,
                "/api",
                "/Categories",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                mainViewModel.CurrentCategory.CategoryId);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowErrorMessage(response.Message);
                return;
            }
            generalItems = (List<GeneralItem>)response.Result;
            GeneralItems = new ObservableCollection<GeneralItem>(generalItems.OrderBy(c => c.Name));
            IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadGeneralItems);
            }
        }
        #endregion
    }
}
