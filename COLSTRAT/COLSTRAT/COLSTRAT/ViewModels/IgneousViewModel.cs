namespace COLSTRAT.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using COLSTRAT.Service;
    using System.Collections.ObjectModel;
    using COLSTRAT.Models;
    using System.Collections.Generic;
    using COLSTRAT.Helpers;

    public class IgneousViewModel : INotifyPropertyChanged
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
        private int _index;
        private bool _isRunning;
        private bool _IsEnabled;
        private ObservableCollection<IgneousRock> _igneousRocks;
        private IgneousRock _sourceRock;
        private string _descripcion;
        private string _minerals;
        private string _imageSource;
        private string _statusLoad;
        #endregion

        #region Properties
        public string StatusLoad
        {
            get
            {
                return _statusLoad;
            }
            set
            {
                if (_statusLoad != value)
                {
                    _statusLoad = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(StatusLoad)));
                }
            }
        }
        public IgneousRock SourceRock
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
        public ObservableCollection<IgneousRock> IgneousRocks
        {
            get
            {
                return _igneousRocks;
            }
            set
            {
                if (_igneousRocks != value)
                {
                    _igneousRocks = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IgneousRocks)));
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
                if(_IsEnabled != value)
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
        public IgneousViewModel()
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
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                await dialogService.ShowMessage(Languages.Warning, connection.Message);
                return;
            }
            var url = "http://192.168.0.105:3000";
            var controller = "/igneous_rocks";
            var response = await apiService.GetList<IgneousRock>(url,controller);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                await dialogService.ShowMessage(Languages.Warning, response.Message);
                return;
            }
            IgneousRocks = new ObservableCollection<IgneousRock>((List<IgneousRock>)response.Result);
            IsRunning = false;
            IsEnabled = true;
            StatusLoad = "Rocks loaded from api.";
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
        public ICommand ImageCommand
        {
            get
            {
                return new RelayCommand(ImageDialog);
            }
        }
        async void ImageDialog()
        {
            await dialogService.ShowConfirm(Languages.Message_Image, Languages.Message_Image_Content);
        }
        #endregion
    }
}
