using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COLSTRAT.Models;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using COLSTRAT.Service;
using Newtonsoft.Json;
using System.Net.Http;
using COLSTRAT.Helpers;

namespace COLSTRAT.ViewModels
{
    public class MetamorphicViewModel : INotifyPropertyChanged
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Attributes
        private bool _isRunning;
        private bool _isEnabled;
        private MetamorphicRock _sourceRock;
        private ObservableCollection<MetamorphicRock> _metamorphicRocks;
        private string _descripcion;
        private string _minerals;
        private string _imageSource;
        #endregion
        #region Properties
        public ObservableCollection<MetamorphicRock> MetamorphicRocks
        {
            get
            {
                return _metamorphicRocks;
            }
            set
            {
                if (_metamorphicRocks != value)
                {
                    _metamorphicRocks = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(MetamorphicRocks)));
                }
            }
        }
        public MetamorphicRock SourceRock
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
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnabled)));
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
        #endregion

        #region Constructor
        public MetamorphicViewModel()
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
            var controller = "/metamorphic_rocks";
            var response = await apiService.GetRocks(controller);
            if (!response.IsSuccess)
            {
                IsRunning = false;
                await dialogService.ShowMessage("ERROR", response.Message);
            }
            var rocks = JsonConvert.DeserializeObject<List<MetamorphicRock>>(response.Result.ToString());
            MetamorphicRocks = new ObservableCollection<MetamorphicRock>(rocks);
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
