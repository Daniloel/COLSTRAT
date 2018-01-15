namespace COLSTRAT.ViewModels
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Models;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels.Login;
    using COLSTRAT.ViewModels.Main;
    using COLSTRAT.ViewModels.Rocks;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public class MainViewModel : INotifyPropertyChanged
    {
        #region Services
        private NavigationService navigationService;
        #endregion

        #region Attributes
        MainMenu _currentMenu;
        string _titlePage;
        #endregion
        #region Properties
        public string TitlePage
        {
            get { return _titlePage; }
            set
            {
                if (_titlePage != value)
                {
                    _titlePage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitlePage)));
                }
            }
        }
        public MainMenu CurrentMenu
        {
            get;
            set;
        }
        public Category CurrentCategory
        {
            get;
            set;
        }
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
        public MainMenuViewModel MainMenu { get; set; }
        public RocksViewModel Rocks { get; set; }
        public CategoryMenuViewModel CategoryMenu { get; set; }
        public NewCategoryViewModel NewCategory { get; set; }
        public NewMenuViewModel NewMenu { get; set; }
        public EditCategoryViewModel EditCategory { get; set; }
        public EditMenuViewModel EditMenu { get; set; }
        public GeneralItemViewModel GeneralItem { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;

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
        public ICommand NewMenuCommand
        {
            get
            {
                return new RelayCommand(GoNewMenu);
            }
        }

        async void GoNewMenu()
        {
            NewMenu = new NewMenuViewModel();
            await navigationService.Navigate("NewMenuView");
        }

        public ICommand NewCategoryCommand
        {
            get
            {
                return new RelayCommand(GoNewCategory);
            }
        }

        async void GoNewCategory()
        {
            NewCategory = new NewCategoryViewModel();
            NewCategory.MainMenu = CurrentMenu;
            await navigationService.Navigate("NewCategoryView");
        }

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
        public ICommand ToTypesOfRocks
        {
            get{ return new RelayCommand(ShowTypesOfRocks); }
        }
        async void ShowTypesOfRocks()
        {
            MainMenu = new MainMenuViewModel();
            await navigationService.Navigate("MainMenuView");
        }
        #endregion

    }
}
