namespace COLSTRAT.ViewModels.Login
{
    using System.ComponentModel;
    using System.Windows.Input;
    using COLSTRAT.Service;
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Xamarin.Forms;

    public class NewCustomerViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DataService dataService;
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Properties
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

        public string FirstName
        {
            get;
            set;
        }


        public string LastName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Address
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

        public string Confirm
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public NewCustomerViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            dataService = new DataService();
            IsEnabled = true;
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

        async void Save()
        {
            if (string.IsNullOrEmpty(FirstName))
            {
                await dialogService.ShowMessage(
                    Languages.Warning,
                    Languages.First_Name_Error);
                return;
            }

            if (string.IsNullOrEmpty(LastName))
            {
                await dialogService.ShowMessage(
                     Languages.Warning,
                    Languages.Last_Name_Error);
                return;
            }

            if (string.IsNullOrEmpty(Email))
            {
                await dialogService.ShowErrorMessage(Languages.ErrorEmailEmpty);
                return;
            }

            if (!RegexUtilities.IsValidEmail(Email))
            {
                await dialogService.ShowMessage(
                     Languages.Warning,
                    Languages.Email_Invalid);
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowErrorMessage(Languages.ErrorPasswordEmpty);
                return;
            }

            if (Password.Length < 6)
            {
                await dialogService.ShowMessage(
                     Languages.Warning,
                    Languages.Pass_Error_Length);
                return;
            }

            if (string.IsNullOrEmpty(Confirm))
            {
                await dialogService.ShowMessage(
                     Languages.Warning,
                    Languages.Pass_Confirm_Error_Input);
                return;
            }

            if (!Password.Equals(Confirm))
            {
                await dialogService.ShowMessage(
                    Languages.Warning,
                    Languages.Pass_Confirm_NotMatch);
                return;
            }

            IsRunning = true;
            IsEnabled = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowErrorMessage(connection.Message);
                return;
            }

            var customer = new Customer
            {
                Address = Address,
                CustomerType = 1,
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Password = Password,
                Phone = Phone,
            };
            string urlBase = Application.Current.Resources["URL_API"].ToString();

            var response = await apiService.Post(
                urlBase,
                "/api",
                "/Customers",
                customer);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                if (response.Message == "EnCt26860b49624d62f36e9b46f0a7709351ff8aebaf06860b49624d62f36e9b46f0aU4wcPuWMzgC3BCcYXVq1bOOqTDUvH9HZAVc3bNG0Lon1063ZHzvobHDr4LaHoE")
                {
                    await dialogService.ShowMessage(
                    Languages.Warning,
                    Languages.Email_Exists);
                }
                else
                {
                    await dialogService.ShowMessage(
                     Languages.Warning,
                    response.Message);
                }
                
                return;
            }

            
            var response2 = await apiService.GetToken(
                urlBase,
                Email,
                Password);

            if (response2 == null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                     Languages.Warning,
                    Languages.ErrorResponseNotFound);
                Password = null;
                return;
            }

            if (string.IsNullOrEmpty(response2.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    response2.ErrorDescription);
                Password = null;
                return;
            }
            var mainViewModel = MainViewModel.GetInstante();
            response2.IsRemembered = true;
            response2.Password = Password;
            dataService.DeleteAllAndInsert(response2);
            mainViewModel.CurrentCustomer = (Customer)response.Result;
            dataService.DeleteAllAndInsert(mainViewModel.CurrentCustomer);
            mainViewModel.Token = response2;
            mainViewModel.Menu = new MenuItemViewModel();
            mainViewModel.MainMenu = new MainMenuViewModel();
            await navigationService.BackOnLogin();
            navigationService.SetMainPage("MasterView");
            IsRunning = false;
            IsEnabled = true;
        }
        #endregion

    }
}
