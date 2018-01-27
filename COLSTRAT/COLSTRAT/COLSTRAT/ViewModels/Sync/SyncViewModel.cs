namespace COLSTRAT.ViewModels.Sync
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using COLSTRAT.Service;
    using COLSTRAT.Helpers;
    using Xamarin.Forms;
    using System;
    using COLSTRAT.Models;
    using System.Linq;

    public class SyncViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        DataService dataService;
        #endregion

        #region Attributes
        private bool _isRunning;
        private bool _isEnabled;
        private string _message;
        #endregion


        #region Properties
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
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message)));
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
        #endregion
        #region Constructor
        public SyncViewModel()
        {
            IsEnabled = true;
            Message = Languages.Label_Sync_Info;
            dialogService = new DialogService();
            apiService = new ApiService();
            dataService = new DataService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand SyncCommand
        {
            get
            {
                return new RelayCommand(SyncData);
            }
        }

        async void SyncData()
        {
            
            var conn = await apiService.CheckConnection();
            if (!conn.IsSuccess)
            {
                await dialogService.ShowErrorMessage(conn.Message);
                return;
            }
            IsEnabled = false;
            IsRunning = true;
            var generalItems = dataService.Get<GeneralItem>(false)
                .Where(p => p.PendingToSave).ToList();
            if (generalItems.Count == 0)
            {
                await dialogService.ShowErrorMessage(Languages.Sync_Not_Data);
                IsEnabled = true;
                IsRunning = false;
                return;
            }
            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            foreach (var item in generalItems)
            {
                var response = await apiService.Post(
                urlBase,
                "/api",
                "/GeneralItems",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                item);
                if (response.IsSuccess)
                {
                    item.PendingToSave = false;
                    dataService.Update(item);
                }
            }
            IsEnabled = true;
            IsRunning = false;
            await dialogService.ShowMessage(Languages.Sync_Success);
            await navigationService.Back();
        }
        #endregion
    }
}
