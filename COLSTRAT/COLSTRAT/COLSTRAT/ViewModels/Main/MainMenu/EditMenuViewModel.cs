using COLSTRAT.Helpers;
using COLSTRAT.Models;
using COLSTRAT.Service;
using GalaSoft.MvvmLight.Command;
using Plugin.Iconize;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace COLSTRAT.ViewModels.Main
{
    public class EditMenuViewModel : INotifyPropertyChanged
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
        string _description;
        private bool _isRunning;
        private bool _isEnabled;
        private MainMenu mainMenu;
        string _icon;
        List<IIcon> _iconsApp;
        Icon _iconSelected;
        #endregion

        #region Properties
        public Icon IconSelected
        {
            get
            {
                return _iconSelected;
            }
            set
            {
                if (_iconSelected != value)
                {
                    _iconSelected = value;
                    Icon = IconSelected.Key;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconSelected)));
                }
            }
        }
        public List<IIcon> IconsApp
        {
            get
            {
                return _iconsApp;
            }
            set
            {
                if (_iconsApp != value)
                {
                    _iconsApp = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IconsApp)));
                }
            }
        }
        public string Icon
        {
            get { return _icon; }
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon)));
                }
            }
        }
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }


        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }
        #endregion

        #region Constructor
        public EditMenuViewModel(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            Description = mainMenu.Description;
            Icon = mainMenu.Icon;
            IsEnabled = true;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            IconsApp = COLSTRAT.Service.Icons.COLSTRATIconsCollection.Icons.ToList();

        }
        #endregion


        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage(Languages.Warning, Languages.Error_Input_Menu);
            }
            IsRunning = true;
            IsEnabled = false;
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                await dialogService.ShowErrorMessage(con.Message);
                IsRunning = false;
                IsEnabled = true;
                return;
            }

            mainMenu.Description = Description;
            mainMenu.Icon = Icon;
            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            var response = await apiService.Put(
                urlBase,
                "/api",
                "/MainMenus",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                mainMenu);

            if (!response.IsSuccess)
            {
                if (response.Message == "1oGVEdBYMPQ2yLGq3HnZOzYFmOtfErKHYtyLPO95mdf/BbS7b1DYbDgiMJQi/blDoVi/I1NSS9Ria3sOeX3wOaBCZGatrfNiI4rjkM3XYw8")
                {
                    await dialogService.ShowErrorMessage(Languages.Error_Record_Same);
                }
                else
                {
                    await dialogService.ShowErrorMessage(Languages.ErrorResponseNotFound);
                }
                IsRunning = false;
                IsEnabled = true;
                return;
            }

            MainMenuViewModel mainMenuViewModel = MainMenuViewModel.GetInstante();
            mainMenuViewModel.UpdateMenu(mainMenu);
            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
