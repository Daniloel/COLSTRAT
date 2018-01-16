namespace COLSTRAT.ViewModels
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Models;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels.Login;
    using COLSTRAT.ViewModels.Main;
    using COLSTRAT.ViewModels.Main.GeneralItem;
    using COLSTRAT.ViewModels.Maps;
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
        public LoginViewModel Login { get; set; }
        public TokenResponse Token { get; set; }
        public MainMenuViewModel MainMenu { get; set; }
        public RocksMenuViewModel RocksMenu { get; set; }
        public RocksViewModel Rocks { get; set; }
        public CategoryMenuViewModel CategoryMenu { get; set; }
        public NewCategoryViewModel NewCategory { get; set; }
        public NewMenuViewModel NewMenu { get; set; }
        public EditCategoryViewModel EditCategory { get; set; }
        public EditMenuViewModel EditMenu { get; set; }
        public GeneralItemViewModel GeneralItem { get; set; }
        public EditGeneralItemViewModel EditGeneralItem { get; set; }
        public NewGeneralItemViewModel NewGeneralItem { get; set; }
        public NewCustomerViewModel NewCustomer { get; set; }
        public UbicationsViewModel Ubications { get; set; }
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
                Icon = "fa-home",
                PageName = "Home",
                Title = Languages.Home
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "fa-user",
                PageName = "MyProfileView",
                Title =Languages.Profile,
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "fa-map-marker",
                PageName = "UbicationsView",
                Title = Languages.Ubication,
            });
            Menu.Add(new MenuItemViewModel
            {
                Icon = "fa-sign-out",
                PageName = "LogoutView",
                Title = Languages.btnExit
            });

            

        }
        #endregion

            #region Commands
        public ICommand NewRockCommand
        {
            get
            {
                return new RelayCommand(GoNewRock);
            }
        }

        async void GoNewRock()
        {
          /*  NewRock = new NewRockViewModel();
            await navigationService.Navigate("NewRock");*/
        }
        public ICommand NewRockMenuCommand
        {
            get
            {
                return new RelayCommand(GoNewRockMenu);
            }
        }

        async void GoNewRockMenu()
        {
            /*  NewRock = new NewRockViewModel();
              await navigationService.Navigate("NewRock");*/
        }
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
        public ICommand NewGeneralItemCommand
        {
            get
            {
                return new RelayCommand(GoGeneralItem);
            }
        }

        async void GoGeneralItem()
        {
            NewGeneralItem = new NewGeneralItemViewModel();
            NewGeneralItem.Category = CurrentCategory;
            await navigationService.Navigate("NewGeneralItemView");
        }
        #endregion

    }
}
