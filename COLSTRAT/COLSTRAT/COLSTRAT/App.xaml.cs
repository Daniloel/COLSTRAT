namespace COLSTRAT
{
    using Xamarin.Forms;
    using COLSTRAT.Views.Login;
    using COLSTRAT.Resources;
    using System;
    using COLSTRAT.Models.Login;
    using COLSTRAT.Views;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels;
    using COLSTRAT.Helpers;
    using COLSTRAT.Models;

    public partial class App : Application
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        NavigationService navigationService;
        #endregion


        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterDetailPage Master { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            WayToInit();
        }
        #endregion

        #region Methods
        void WayToInit()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            dataService = new DataService();
            navigationService = new NavigationService();

            var token = dataService.First<TokenResponse>(false);
            var customer = dataService.First<Customer>(false);
            if (token != null &&
                token.IsRemembered &&
                token.Expires > DateTime.Now &&
                customer != null &&
                customer.Email == token.UserName)
            {
                var mainViewModel = MainViewModel.GetInstante();
                mainViewModel.Token = token;
                mainViewModel.CurrentCustomer = customer;
                mainViewModel.Menu = new MenuItemViewModel();
                mainViewModel.MainMenu = new MainMenuViewModel();
                navigationService.SetMainPage("MasterView");
            }
            else
            {
                navigationService.SetMainPage("LoginView");
            }
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Action LoginFacebookFail
        {
            get
            {
                return new Action(() => Current.MainPage =
                                  new NavigationPage(new LoginView()){ BarBackgroundColor = Colors.MainColor });
            }
        }

        public async static void LoginFacebookSuccess(FacebookResponse profile)
        {
            if (profile == null)
            {
                Current.MainPage = new NavigationPage(new LoginView()) { BarBackgroundColor = Colors.MainColor };
                return;
            }
            var apiService = new ApiService();
            var dialogService = new DialogService();
            var dataService = new DataService();
            var checkConnetion = await apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                await dialogService.ShowErrorMessage(checkConnetion.Message);
                return;
            }

            var urlAPI = Current.Resources["URL_API"].ToString();
            var token = await apiService.LoginFacebook(
                urlAPI,
                "/api",
                "/Customers/LoginFacebook",
                profile);

            if (token == null)
            {
                await dialogService.ShowMessage(
                    Languages.Warning,
                    Languages.User_Error_Info);
                Current.MainPage = new NavigationPage(new LoginView()) { BarBackgroundColor = Colors.MainColor };
                return;
            }
            Customer customer = new Customer
            {
                Email = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                CustomerType = 2,
                Password = profile.Id
            };
            token.IsRemembered = true;
            dataService.DeleteAllAndInsert(token);
            dataService.DeleteAllAndInsert(customer);
            var mainViewModel = MainViewModel.GetInstante();
            mainViewModel.Token = token;
            mainViewModel.Menu = new MenuItemViewModel();
            mainViewModel.MainMenu = new MainMenuViewModel();
            Current.MainPage = new MasterView();

        }

        #endregion
    }
}
