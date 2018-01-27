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
using COLSTRAT.Helpers;

namespace COLSTRAT.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DataService dataService;
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Attributes
        bool _isRefreshing;
        List<MainMenu> mainMenu;
        ObservableCollection<MainMenu> _mainMenuItems;
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
        public ObservableCollection<MainMenu> MainMenuItems
        {
            get { return _mainMenuItems; }
            set
            {
                if (_mainMenuItems != value)
                {
                    _mainMenuItems = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainMenuItems)));
                }
            }
        }
        #endregion

        #region Constructor
        public MainMenuViewModel()
        {
            instance = this;
            dataService = new DataService();
            dialogService = new DialogService();
            apiService = new ApiService();
            LoadMainMenu();
        }
        #endregion


        #region Singleton
        static MainMenuViewModel instance;
        
        public static MainMenuViewModel GetInstante()
        {
            if (instance == null)
            {
                return new MainMenuViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommandMainMenu
        {
            get
            {
                return new RelayCommand(LoadMainMenu);
            }
        }
        #endregion

        #region Methods
        public void AddMenu(MainMenu mainmenu)
        {
            IsRefreshing = true;
            mainMenu.Add(mainmenu);
            MainMenuItems = new ObservableCollection<MainMenu>(mainMenu.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        public void UpdateMenu(MainMenu mainmenu)
        {
            IsRefreshing = true;
            var oldMenu = mainMenu.Where(c => c.MainMenuId == mainmenu.MainMenuId).FirstOrDefault();
            oldMenu = mainmenu;
            MainMenuItems = new ObservableCollection<MainMenu>(mainMenu.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        public async void DeleteMenu(MainMenu mainmenu)
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
            var response = await apiService.Delete(
                urlBase,
                "/api",
                "/MainMenus",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                mainmenu);

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
            mainMenu.Remove(mainmenu);
            MainMenuItems = new ObservableCollection<MainMenu>(mainMenu.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        private async void LoadMainMenu()
        {
            IsRefreshing = true;
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                mainMenu = dataService.Get<MainMenu>(true);
                if (mainMenu.Count == 0)
                {
                    IsRefreshing = false;
                    await dialogService.ShowErrorMessage(Languages.Message_Not_Data);
                    return;
                }
                MainMenuItems = new ObservableCollection<MainMenu>(mainMenu.OrderBy(c => c.Description));
            }
            else
            {
                string urlBase = Application.Current.Resources["URL_API"].ToString();
                var mainViewModel = MainViewModel.GetInstante();
                var response = await apiService.GetList<MainMenu>(
                    urlBase,
                    "/api",
                    "/MainMenus",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken);

                if (!response.IsSuccess)
                {
                    IsRefreshing = false;
                    await dialogService.ShowErrorMessage(response.Message);
                    return;
                }

                mainMenu = (List<MainMenu>)response.Result;
                SaveMainMenusOnDB();
                MainMenuItems = new ObservableCollection<MainMenu>(mainMenu.OrderBy(c => c.Description));
            }
            IsRefreshing = false;
        }


        private void SaveMainMenusOnDB()
        {
            dataService.DeleteAll<MainMenu>();
            foreach (var menu in mainMenu)
            {
                dataService.Insert(menu);
                dataService.Save(menu.Category);
            }
        }
        #endregion



    }
}
