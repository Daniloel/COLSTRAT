namespace COLSTRAT.ViewModels.Profile
{
    using System;
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
        SessionService session;
        NavigationService navigationService;
        #endregion

        #region Attributes
        private string _displayName;
        private string _email;
        #endregion

        #region Properties

        public string DisplayName
        {
            get { return _displayName; }
            set {
                if (_displayName != value)
                {
                    _displayName = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(DisplayName)));
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
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }
        #endregion

        #region Constructors
        public MyProfileViewModel()
        {
            session = new SessionService();
            navigationService = new NavigationService();
            SetUserProfile();
        }
        #endregion

        #region Methods
        void SetUserProfile()
        {
            var user = session.GetCurrentUser();
            Email = user.Email;
            DisplayName = user.FirstName + " " + user.LastName;
        }
        #endregion

        #region Commands
        public ICommand ChangePasswordCommand
        {
            get
            {
                return new RelayCommand(ChangePassword);
            }
        }

        async void ChangePassword()
        {
            MainViewModel.GetInstante().ChangePassword = new ChangePasswordViewModel();
            await navigationService.Navigate("ChangePasswordView");
        }
        public ICommand InfoAppCommand
        {
            get
            {
                return new RelayCommand(ToInfoApp);
            }
        }

        private async void ToInfoApp()
        {
            MainViewModel.GetInstante().About = new AboutViewModel();
            await navigationService.NavigateModal("AboutView");
        }
        public ICommand TermsCommand
        {
            get
            {
                return new RelayCommand(ToTermsAndPrivacy);
            }
        }

        private void ToTermsAndPrivacy()
        {
            Device.OpenUri(new Uri("http://colstrat.somee.com/Home/Legal"));
        }
        #endregion
    }

}
