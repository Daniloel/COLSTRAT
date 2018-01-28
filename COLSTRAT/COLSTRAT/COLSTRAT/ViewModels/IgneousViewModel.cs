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
    using Xamarin.Forms;
    using System;
    using System.Threading.Tasks;

    public class IgneousViewModel : INotifyPropertyChanged
    {
        #region Services
        private ApiService apiService;
        private DialogService dialogService;
        private DataService dataService;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Attributes
        List<IgneousRock> rocks;
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
            dataService = new DataService();
            dialogService = new DialogService();
          //  Load();
        }
        #endregion

        #region Methods
        async void Load()
        {
      /*      IsRunning = true;
            IsEnabled = false;
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                LoadLocalData();
            }
            else
            {
                await LoadDataFromAPI();
            }
            if (rocks.Count == 0)
            {
                IsRunning = false;
                IsEnabled = false;
                await dialogService.ShowMessage(Languages.Warning, 
                    "There are not internet connection " +
                    "and not load previously rocks. " +
                    "Please try again with internet connection");
                StatusLoad = "No rocks loaded";
                return;
            }
            IgneousRocks = new ObservableCollection<IgneousRock>(rocks);
            IsRunning = false;
            IsEnabled = true;*/
        }

        void LoadLocalData()
        {
            rocks = dataService.Get<IgneousRock>(false);
            StatusLoad = Languages.Loaded_From_Local;
        }

        async Task LoadDataFromAPI()
        {
       /*     var url = Application.Current.Resources["URL_API"].ToString();
            var controller = "/igneous_rocks";
            var response = await apiService.GetList<IgneousRock>(url, controller);
            if (!response.IsSuccess)
            {
                LoadLocalData();
                return;
            }
            rocks = (List<IgneousRock>)response.Result;
            dataService.DeleteAll<IgneousRock>();
            dataService.Save(rocks);
            StatusLoad = Languages.Loaded_From_Api;*/
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
         /*   if (SourceRock == null)
            {
                await dialogService.ShowMessage(Languages.Warning, Languages.Message_Not_Select_Rock);
                return;
            }
            Descripcion = SourceRock.Descripcion;
            Minerals = SourceRock.Minerals_Composition;
            ImageSource = SourceRock.Image;*/
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
