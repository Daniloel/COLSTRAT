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
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
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
            mainMenu.Add(mainmenu);
            MainMenuItems = new ObservableCollection<MainMenu>(mainMenu.OrderBy(c => c.Description));
        }

        private async void LoadMainMenu()
        {
            IsRefreshing = true;
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                await dialogService.ShowErrorMessage(con.Message);
                return;
            }
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
                await dialogService.ShowErrorMessage(response.Message);
                return;
            }

            mainMenu = (List<MainMenu>)response.Result;
            MainMenuItems = new ObservableCollection<MainMenu>(mainMenu.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        #endregion

        

    }
}
