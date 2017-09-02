namespace COLSTRAT.ViewModels
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Service;
    using COLSTRAT.Views.Rocks;
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
        #endregion

        #region Constructor
        public MainViewModel()
        {
            Menu = new ObservableCollection<MenuItemViewModel>();
            navigationService = new NavigationService();
            LoadMenu();
        }
        #endregion
        #region Methods
        private void LoadMenu()
        {
            Menu.Add(new MenuItemViewModel
            {
                Icon = "",
                PageName = "IgneousView",
                Title = Languages.IgneousRocks
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "",
                PageName = "MetamorphicView",
                Title = Languages.MetamorphicRocks
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "",
                PageName = "SedimentaryView",
                Title = Languages.SedimentaryRocks
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "",
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
        async void ShowIgneousRocks()
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
