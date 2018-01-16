namespace COLSTRAT.ViewModels.Login
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using COLSTRAT.Service;
    using COLSTRAT.Helpers;
    using Xamarin.Forms;

    public class LoginViewModel : INotifyPropertyChanged
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
        private string _email;
        private string _password;
        private bool _isToggled;
        private bool _isRunning;
        private bool _isEnabled;
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


        public bool IsToggled
        {
            get { return _isToggled; }
            set
            {
                if (_isToggled != value)
                {
                    _isToggled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }


        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }

            }
        }


        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        } 
        #endregion


        #region Constructor
        public LoginViewModel()
        {
            IsEnabled = true;
            IsToggled = true;
            dialogService = new DialogService();
            apiService = new ApiService();
            navigationService = new NavigationService();
            Email = "danieldaniyyelda@gmail.com";
            Password = "123456";
        }
        #endregion

        #region Commands
        public ICommand RegisterNewUserCommand
        {
            get
            {
                return new RelayCommand(RegisterNewUser);
            }
        }

        async void RegisterNewUser()
        {
            MainViewModel.GetInstante().NewCustomer = new NewCustomerViewModel();
            await navigationService.NavigateOnLogin("NewCustomerView");
        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            
            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowErrorMessage(Languages.ErrorEmailEmpty);
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowErrorMessage(Languages.ErrorPasswordEmpty);
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
            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var response = await apiService.GetToken(urlBase,Email, Password);

            if (response == null)
            {
                await dialogService.ShowErrorMessage(Languages.ErrorResponseNotFound);
                IsRunning = false;
                IsEnabled = true;
                return;
            }

            if (response == null || string.IsNullOrEmpty(response.AccessToken))
            {
                await dialogService.ShowErrorMessage(response.ErrorDescription);
                IsRunning = false;
                IsEnabled = true;
                return;
            }
            var mainViewModel = MainViewModel.GetInstante();
            mainViewModel.Token = response;
            mainViewModel.MainMenu = new MainMenuViewModel();
            navigationService.SetMainPage("MasterView");
            IsRunning = false;
            IsEnabled = true;
            Email = null;
            Password = null;

        }
        #endregion
    }
}
