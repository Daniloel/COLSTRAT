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

namespace COLSTRAT.ViewModels.Rocks
{
    public class RocksMenuViewModel : INotifyPropertyChanged
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
        List<RocksMenu> rocks;
        ObservableCollection<RocksMenu> _rocksMenu;
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
        public ObservableCollection<RocksMenu> RocksMenu
        {
            get { return _rocksMenu; }
            set
            {
                if (_rocksMenu != value)
                {
                    _rocksMenu = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RocksMenu)));
                }
            }
        }
        #endregion

        #region Constructor
        public RocksMenuViewModel()
        {
            instance = this;
            dataService = new DataService();
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            LoadRocks();
        }
        #endregion

        #region Singleton
        static RocksMenuViewModel instance;

        public static RocksMenuViewModel GetInstante()
        {
            if (instance == null)
            {
                return new RocksMenuViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods

        private async void LoadRocks()
        {
            IsRefreshing = true;
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                rocks = dataService.Get<RocksMenu>(true);
                if (rocks.Count == 0)
                {
                    IsRefreshing = false;
                    await dialogService.ShowErrorMessage(Languages.Message_Not_Data);
                    return;
                }
                RocksMenu = new ObservableCollection<RocksMenu>(rocks.
                    OrderBy(c => c.Name));
            }
            else
            {
                string urlBase = Application.Current.Resources["URL_API"].ToString();
                var mainViewModel = MainViewModel.GetInstante();
                var response = await apiService.GetList<Category>(
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
                var category = (List<Category>)response.Result;
                rocks = category.First().RocksMenu;
                SaveRocksMenuOnDB();
                RocksMenu = new ObservableCollection<RocksMenu>(rocks.OrderBy(c => c.Name));
            }
            
            IsRefreshing = false;
        }

        private void SaveRocksMenuOnDB()
        {
            dataService.DeleteAll<RocksMenu>();
            foreach (var rock in rocks)
            {
                dataService.Insert(rock);
                dataService.Save(rock.Rocks);
            }
        }

        public void AddMenu(RocksMenu rocksmenu)
        {
            IsRefreshing = true;
            rocks.Add(rocksmenu);
            RocksMenu = new ObservableCollection<RocksMenu>(rocks.OrderBy(c => c.Name));
            IsRefreshing = false;
        }
        public void UpdateMenu(RocksMenu rocksmenu)
        {
            IsRefreshing = true;
            var oldItem = rocks.Where(c => c.RocksMenuId == rocksmenu.RocksMenuId).FirstOrDefault();
            oldItem = rocksmenu;
            RocksMenu = new ObservableCollection<RocksMenu>(rocks.OrderBy(c => c.Name));
            IsRefreshing = false;
        }
        public async void DeleteCategory(RocksMenu rocksmenu)
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
                "/RocksMenus",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                rocksmenu);

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
            rocks.Remove(rocksmenu);

            RocksMenu = new ObservableCollection<RocksMenu>(rocks.OrderBy(c => c.Name));
            IsRefreshing = false;
        }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadRocks);
            }
        }
        #endregion

    }
}
