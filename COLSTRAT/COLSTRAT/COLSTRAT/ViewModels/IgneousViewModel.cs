using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLSTRAT.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.Net.Http;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class IgneousViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Attributes
        bool _IsRunning;
        bool _IsEnable;
        ObservableCollection<IgneousRock> _IgneousRocks;
        #endregion
        #region Properties
        /// <summary>
        /// La propiedad contiene una coleccion de rocas igneas
        /// </summary>
        /// <return></return>
        public ObservableCollection<IgneousRock> IgneousRocks
        {
            get
            {
                return _IgneousRocks;
            }
            set
            {
                if (_IgneousRocks != value)
                {
                    _IgneousRocks = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IgneousRocks)));
                }
            }
        }
        public IgneousRock SourceRock { get; set; }
        public bool IsRunning
        {
            get
            {
                return _IsRunning;
            }
            set
            {
                if (_IsRunning != value)
                {
                    _IsRunning = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }
        public bool IsEnable
        {
            get
            {
                return _IsEnable;
            }
            set
            {
                if(_IsEnable != value)
                {
                    _IsEnable = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(IsEnable)));
                }
            }
        }
        #endregion

        #region Contructor
        public IgneousViewModel()
        {
            LoadIgneousRocks();
        }
        #endregion

        #region Methods
        async void LoadIgneousRocks()
        {
            IsRunning = true;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:3000");
                var controller = "/igneous_rocks";
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    IsRunning = false;
                }
                var rocks = JsonConvert.DeserializeObject<List<IgneousRock>>(result);
                IgneousRocks = new ObservableCollection<IgneousRock>(rocks);
                IsRunning = false;
                IsEnable = true;
            }
            catch (Exception ex)
            {
                IsRunning = false;
            }
        }
        #endregion
        #region Commands

        public ICommand ShowCommand
        {
            get
            {
                return new RelayCommand(Show());
            }
        }

        private Action Show()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
