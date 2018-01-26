using COLSTRAT.Helpers;
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
        string _filter;
        #endregion

        #region Properties
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    SearchItem();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
                }
            }
        }
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
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            LoadGeneralItems();
        }
        #endregion

        #region Singleton
        static GeneralItemViewModel instance;
      
        public static GeneralItemViewModel GetInstante()
        {
            if (instance == null)
            {
                return new GeneralItemViewModel();
            }

            return instance;
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

        public void AddMenu(GeneralItem generalitem)
        {
            IsRefreshing = true;
            generalItems.Add(generalitem);
            GeneralItems = new ObservableCollection<GeneralItem>(generalItems.OrderBy(c => c.Name));
            IsRefreshing = false;
        }
        public void UpdateMenu(GeneralItem generalitem)
        {
            IsRefreshing = true;
            var oldItem = generalItems.Where(c => c.GeneralItemId == generalitem.GeneralItemId).FirstOrDefault();
            oldItem = generalitem;
            GeneralItems = new ObservableCollection<GeneralItem>(generalItems.OrderBy(c => c.Name));
            IsRefreshing = false;
        }
        public async void DeleteCategory(GeneralItem generalitem)
        {
            IsRefreshing = true;
            apiService = new ApiService();
            dialogService = new DialogService();
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowErrorMessage(con.Message);
                return;
            }

            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            var response = await apiService.Delete(
                urlBase,
                "/api",
                "/GeneralItems",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                generalitem);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                if (response.Message == "mdg4ymQsXUPdMYLR74DMSqdwMdppHC1yssL5+SuIvJ8B3a7Pf2PIBULCV1+0oQEXewaNRYU09w76N1tktNaPxQ==")
                {
                    await dialogService.ShowErrorMessage(Languages.Error_Record_Relateds);
                }
                else
                {
                    await dialogService.ShowErrorMessage(Languages.ErrorResponseNotFound);
                }
                return;
            }
            generalItems.Remove(generalitem);

            GeneralItems = new ObservableCollection<GeneralItem>(generalItems.OrderBy(c => c.Name));
            IsRefreshing = false;
        }

        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(SearchItem);
            }
        }

        private void SearchItem()
        {
            IsRefreshing = true;
            if (string.IsNullOrEmpty(Filter))
            {
                GeneralItems = new ObservableCollection<GeneralItem>
                    (generalItems.OrderBy(c => c.Name));
            }
            else
            {
                GeneralItems = new ObservableCollection<GeneralItem>(generalItems
                .Where(c => c.Description != null && c.Description.ToLower().Contains(Filter.ToLower()) ||
                c.Name != null && c.Name.ToLower().Contains(Filter.ToLower()))
                .OrderBy(c => c.Name));
            }
            
            IsRefreshing = false;
        }

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
