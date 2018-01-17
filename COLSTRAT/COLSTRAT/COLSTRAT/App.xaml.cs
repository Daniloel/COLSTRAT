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

    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterDetailPage Master { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView()) { BarBackgroundColor=Colors.MainColor };
        }
        #endregion

        #region Methods
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

            var checkConnetion = await apiService.CheckConnection();
            if (!checkConnetion.IsSuccess)
            {
                await dialogService.ShowErrorMessage(checkConnetion.Message);
                return;
            }

            var urlAPI = Current.Resources["URLAPI"].ToString();
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
                Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var mainViewModel = MainViewModel.GetInstante();
            mainViewModel.Token = token;
            mainViewModel.MainMenu = new MainMenuViewModel();
            Current.MainPage = new MasterView();

        }

        #endregion
    }
}
