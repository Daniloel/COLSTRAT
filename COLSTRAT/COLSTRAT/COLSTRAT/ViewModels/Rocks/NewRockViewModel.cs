﻿namespace COLSTRAT.ViewModels.Rocks
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Models;
    using COLSTRAT.Service;
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class NewRockViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ImageService imageService;
        DataService dataService;
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        string _description;
        private bool _isRunning;
        private bool _isEnabled;
        string _name;
        string _imageFullPath;
        ImageSource _imageSource;
        MediaFile file;
        #endregion

        #region Properties
        public ImageSource ImageSource
        {
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }
            get
            {
                return _imageSource;
            }
        }

        public string ImageFullPath
        {
            get { return _imageFullPath; }
            set
            {
                if (_imageFullPath != value)
                {
                    _imageFullPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageFullPath)));
                }
            }
        }
        public RocksMenu RockMenu
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
        public NewRockViewModel()
        {
            IsEnabled = true;
            dataService = new DataService();
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            imageService = new ImageService();
            ImageSource = imageService.ContentNotAvailable;
        }
        #endregion


        #region Commands
        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await dialogService.ShowImageOptions();

                if (source == Languages.Cancel)
                {
                    file = null;
                    return;
                }

                if (source == Languages.From_Camera)
                {
                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Sample",
                            Name = "test.jpg",
                            PhotoSize = PhotoSize.Small,
                        }
                    );
                }
                else
                {
                    file = await CrossMedia.Current.PickPhotoAsync();
                }
            }
            else
            {
                file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (file != null)
            {
                ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });
            }
        }



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
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage(Languages.Warning, Languages.Description_Required);
                return;
            }
            IsRunning = true;
            IsEnabled = false;
            var con = await apiService.CheckConnection();

            byte[] imageArray = null;
            if (file != null)
            {
                imageArray = FilesHelper.ReadFully(file.GetStream());
                file.Dispose();
            }

            Rock rock = new Rock()
            {
                RocksMenuId = RockMenu.RocksMenuId,
                Name = Name,
                ImageArray = imageArray,
                Descripcion = Description,
                MohsScaleId = 1
            };
            if (!con.IsSuccess)
            {
                rock.PendingToSave = true;
                dataService.Insert(rock);
                await dialogService.ShowMessage(Languages.Message_Save_OnLocal);
            }
            else
            {
                string urlBase = Application.Current.Resources["URL_API"].ToString();
                var mainViewModel = MainViewModel.GetInstante();
                var response = await apiService.Post(
                    urlBase,
                    "/api",
                    "/Rocks",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken,
                    rock);

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

                IsRunning = false;
                IsEnabled = true;
                rock = (Rock)response.Result;

            }

            RocksViewModel rocksViewModel = RocksViewModel.GetInstante();
            rocksViewModel.AddMenu(rock);
            await navigationService.Back();
            IsRunning = false;
            IsEnabled = true;
        }
        #endregion

    }
}
