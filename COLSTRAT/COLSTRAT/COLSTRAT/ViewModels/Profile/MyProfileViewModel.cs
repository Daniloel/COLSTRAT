namespace COLSTRAT.ViewModels.Profile
{
    using System.ComponentModel;
    using System.Windows.Input;
    using COLSTRAT.Helpers;
    using COLSTRAT.Models.Profile;
    using COLSTRAT.Service;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class MyProfileViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DataService dataService;
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

        public string CurrentPassword
        {
            get;
            set;
        }


        public string NewPassword
        {
            get;
            set;
        }

        public string ConfirmPassword
        {
            get;
            set;
        }
        #endregion

        #region Constructors
        public MyProfileViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

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
            if (string.IsNullOrEmpty(CurrentPassword))
            {
                await dialogService.ShowErrorMessage(
                    Languages.Error_Input_Current_Pass);
                return;
            }

            var mainViewModel = MainViewModel.GetInstante();

            if (!mainViewModel.Token.Password.Equals(CurrentPassword))
            {
                await dialogService.ShowErrorMessage(
                    Languages.Error_Current_Pass_Invalid);
                return;
            }

            if (string.IsNullOrEmpty(NewPassword))
            {
                await dialogService.ShowErrorMessage(
                    Languages.Error_Input_New_Pass);
                return;
            }

            if (NewPassword.Length < 6)
            {
                await dialogService.ShowErrorMessage(
                    Languages.Error_Lenght_New_Pass);
                return;
            }

            if (string.IsNullOrEmpty(ConfirmPassword))
            {
                await dialogService.ShowErrorMessage(
                    Languages.Error_Confirm_Input_Pass);
                return;
            }

            if (!NewPassword.Equals(ConfirmPassword))
            {
                await dialogService.ShowErrorMessage(
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

            var changePasswordRequest = new ChangePasswordRequest
            {
                CurrentPassword = CurrentPassword,
                Email = mainViewModel.Token.UserName,
                NewPassword = NewPassword,
            };

            var urlAPI = Application.Current.Resources["URL_API"].ToString();

            var response = await apiService.ChangePassword(
                urlAPI,
                "/api",
                "/Customers/ChangePassword",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                changePasswordRequest);

            if (!response.IsSuccess)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowErrorMessage(
                    response.Message);
                return;
            }

            mainViewModel.Token.Password = NewPassword;
            dataService.Update(mainViewModel.Token);

            await dialogService.ShowMessage(
                Languages.Pass_Success_Changed);
            await navigationService.Back();

            IsRunning = false;
            IsEnabled = true;
        }
        #endregion
    }

}
