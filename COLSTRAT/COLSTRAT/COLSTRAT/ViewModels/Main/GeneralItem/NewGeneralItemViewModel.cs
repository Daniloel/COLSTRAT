﻿namespace COLSTRAT.ViewModels.Main.GeneralItem
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Models;
    using COLSTRAT.Service;
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class NewGeneralItemViewModel : INotifyPropertyChanged
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
        string _name;
        string _image;
        #endregion

        #region Properties

        public Category Category
        {
            get;
            set;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
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

        #region Contructor
        public NewGeneralItemViewModel()
        {
            IsEnabled = true;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
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
            if (string.IsNullOrEmpty(Name))
            {
                await dialogService.ShowMessage(Languages.Warning, Languages.ErrorInputCategory);
                return;
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

            GeneralItem generalItem = new GeneralItem()
            {
                CategoryId = Category.CategoryId,
                Name = Name,
                Description = Description
            };

            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            var response = await apiService.Post(
                urlBase,
                "/api",
                "/GeneralItems",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                generalItem);

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

            generalItem = (GeneralItem)response.Result;
            GeneralItemViewModel generalItemViewModel = GeneralItemViewModel.GetInstante();
            generalItemViewModel.AddMenu(generalItem);
            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }
}
