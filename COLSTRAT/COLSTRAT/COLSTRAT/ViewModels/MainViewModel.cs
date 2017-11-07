namespace COLSTRAT.ViewModels
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Models;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels.Login;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MainViewModel
    {
        #region Services
        private NavigationService navigationService;
        #endregion

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu
        {
            get;
            set;
        }
        public IgneousViewModel Igneous { get; set; }
        public MetamorphicViewModel Metamorphic { get; set; }
        public SedimentaryViewModel Sedimentary { get; set; }
        public LoginViewModel Login { get; set; }
        public TokenResponse Token { get; set; }
        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            Login = new LoginViewModel();
            Menu = new ObservableCollection<MenuItemViewModel>();
            navigationService = new NavigationService();
            LoadMenu();
        }
        #endregion

        #region Singleton
        static MainViewModel instance;

        public static MainViewModel GetInstante()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_launcher_igneous",
                PageName = "IgneousView",
                Title = Languages.IgneousRocks
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_launcher_metamorphic",
                PageName = "MetamorphicView",
                Title = Languages.MetamorphicRocks
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_launcher_sedimentary",
                PageName = "SedimentaryView",
                Title = Languages.SedimentaryRocks
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_launcher_exit",
                PageName = "LogoutView",
                Title = Languages.btnExit
            });
        }
        #endregion


        #region Commands

        public ICommand ToIgneousRocks
        {
           get { return new RelayCommand(ShowIgneousRocks); }
        }
        public async void ShowIgneousRocks()
        {
            Igneous = new IgneousViewModel();
            await navigationService.Navigate("IgneousView");
        }
        public ICommand ToMetamorphicRocks
        {
            get { return new RelayCommand(ShowMetamorphicRocks); }
        }
        async void ShowMetamorphicRocks()
        {
            Metamorphic = new MetamorphicViewModel();
            await navigationService.Navigate("MetamorphicView");
        }
        public ICommand ToSedimentaryRocks
        { 
            get { return new RelayCommand(ShowSedimentaryRocks); }
        }
        async void ShowSedimentaryRocks()
        {
            Sedimentary = new SedimentaryViewModel();
            await navigationService.Navigate("SedimentaryView");
        }

        #endregion

    }
}
