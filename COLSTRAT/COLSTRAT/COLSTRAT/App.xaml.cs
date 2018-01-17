namespace COLSTRAT
{
    using Xamarin.Forms;
    using COLSTRAT.Views.Login;
    using COLSTRAT.Resources;
    using System;
    using COLSTRAT.Models.Login;
    using COLSTRAT.Views;

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
                                  new NavigationPage(new LoginView()));
            }
        }

        public static void LoginFacebookSuccess(FacebookResponse profile)
        {
            Current.MainPage = new MasterView();
        }

        #endregion
    }
}
