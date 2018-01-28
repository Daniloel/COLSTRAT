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
        DataService dataService;
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Attributes        
        bool _isRefreshing;
        List<GeneralItem> generalItems;
        ObservableCollection<GeneralItem> _generalItems;
        string _filter;
        string _labelInfo;
        bool _hasData;
        #endregion
        #region Properties
            public string LabelInfo
        {
            get { return _labelInfo; }
            set
            {
                if (_labelInfo != value)
                {
                    _labelInfo = value;
                    SearchItem();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LabelInfo)));
                }
            }
        }
        public bool HasData
        {
            get { return _hasData; }
            set
            {
                if (_hasData != value)
                {
                    _hasData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasData)));
                }
            }
        }
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
        public GeneralItemViewModel(List<GeneralItem> generalItems)
        {
            instance = this;
            dataService = new DataService();
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            if (generalItems != null)
            {
                this.generalItems = generalItems;
                GeneralItems = new ObservableCollection<GeneralItem>(generalItems.OrderBy(p => p.Name));
            }
            CheckData();
        }
        public GeneralItemViewModel()
        {
            instance = this;
            dataService = new DataService();
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
        void CheckData()
        {
            if (generalItems.Count == 0)
            {
                LabelInfo = Languages.Label_Not_Data;
                HasData = true;
            }
            else
            {
                HasData = false;
            }
        }
        private async void LoadGeneralItems()
        {
            HasData = false;
            IsRefreshing = true;
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                generalItems = dataService.Get<GeneralItem>(true)
                    .Where(p=>p.CategoryId.Equals(MainViewModel.GetInstante().CurrentCategory.CategoryId)).ToList();
                if (generalItems.Count == 0)
                {
                    CheckData();
                    IsRefreshing = false;
                    await dialogService.ShowErrorMessage(Languages.Message_Not_Data);
                    await navigationService.Back();
                    return;
                }
            }
            else
            {
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
                SaveGeneralItemsOnDB();
            }
            SearchItem();
            IsRefreshing = false;
        }
        private void SaveGeneralItemsOnDB()
        {
            foreach (var item in generalItems)
            {
                dataService.InsertOrUpdate(item);
            }
        }
        public void AddMenu(GeneralItem generalitem)
        {
            IsRefreshing = true;
            generalItems.Add(generalitem);
            GeneralItems = new ObservableCollection<GeneralItem>(generalItems.OrderBy(c => c.Name));
            CheckData();
            IsRefreshing = false;
        }
        public void UpdateMenu(GeneralItem generalitem)
        {
            IsRefreshing = true;
            var oldItem = generalItems.Where(c => c.GeneralItemId == generalitem.GeneralItemId).FirstOrDefault();
            oldItem = generalitem;
            GeneralItems = new ObservableCollection<GeneralItem>(generalItems.OrderBy(c => c.Name));
            CheckData();
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
            CheckData();
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
                CheckData();
            }
            else
            {
                GeneralItems = new ObservableCollection<GeneralItem>(generalItems
                .Where(c => c.Description != null && c.Description.ToLower().Contains(Filter.ToLower()) ||
                c.Name != null && c.Name.ToLower().Contains(Filter.ToLower()))
                .OrderBy(c => c.Name));
                if (GeneralItems.Count == 0)
                {
                    HasData = true;
                    LabelInfo = Languages.Label_Not_Coincidences;
                }
                else
                {
                    HasData = false;
                }
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
