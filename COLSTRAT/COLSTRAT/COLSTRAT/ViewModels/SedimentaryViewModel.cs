namespace COLSTRAT.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using COLSTRAT.Service;
    using System.Collections.ObjectModel;
    using COLSTRAT.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using COLSTRAT.Helpers;
    using Xamarin.Forms;

    public class SedimentaryViewModel : INotifyPropertyChanged
    {
        #region Services
        private ApiService apiService;
        private NavigationService navigationService;
        private DialogService dialogService;
        #endregion
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Attributes
        private bool _isRunning;
        private bool _IsEnabled;
        private ObservableCollection<SedimentaryRock> _sedimentaryRocks;
        private SedimentaryRock _sourceRock;
        private string _descripcion;
        private string _minerals;
        private string _imageSource;
        #endregion
        #region Properties
        public SedimentaryRock SourceRock
        {
            get
            {
                return _sourceRock;
            }
            set
            {
                if (_sourceRock != value)
                {
                    _sourceRock = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SourceRock)));
                }
            }
        }
        public string ImageSource
        {
            get
            {
                return _imageSource;
            }
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
        }
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Descripcion)));
                }
            }
        }
        public string Minerals
        {
            get
            {
                return _minerals;
            }
            set
            {
                if (_minerals != value)
                {
                    _minerals = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Minerals)));
                }
            }
        }
        public ObservableCollection<SedimentaryRock> SedimentaryRocks
        {
            get
            {
                return _sedimentaryRocks;
            }
            set
            {
                if (_sedimentaryRocks != value)
                {
                    _sedimentaryRocks = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(SedimentaryRocks)));
                }
            }
        }


        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }
            set
            {
                if (_IsEnabled != value)
                {
                    _IsEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }
        #endregion

        #region Contructor
        public SedimentaryViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            Load();
        }
        #endregion

        #region Methods
        async void Load()
        {
            IsRunning = true;
            IsEnabled = false;
            var url = Application.Current.Resources["URL_API"].ToString();
            var controller = "/sedimentary_rocks";
            var response = await apiService.GetList<SedimentaryRock>(url, controller);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                await dialogService.ShowMessage(Languages.Warning, response.Message);
                return;
            }
            SedimentaryRocks = new ObservableCollection<SedimentaryRock>((List<SedimentaryRock>)response.Result);
            IsEnabled = true;
            IsRunning = false;
        }
        #endregion
        #region Commands

        public ICommand ShowCommand
        {
            get
            {
                return new RelayCommand(Show);
            }
        }

        async void Show()
        {
            if (SourceRock == null)
            {
                await dialogService.ShowMessage(Languages.Warning, Languages.Message_Not_Select_Rock);
                return;
            }
            Descripcion = SourceRock.Descripcion;
            Minerals = SourceRock.Minerals;
            ImageSource = SourceRock.Image;
        }
        #endregion
    }
}
