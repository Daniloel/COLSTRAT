namespace COLSTRAT
{
    using Xamarin.Forms;
    using COLSTRAT.Views.Login;
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
            MainPage = new NavigationPage(new LoginView());
            //MainPage = new MasterView();
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
        #endregion
    }
}
