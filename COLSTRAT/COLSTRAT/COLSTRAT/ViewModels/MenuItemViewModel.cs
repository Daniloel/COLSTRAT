namespace COLSTRAT.ViewModels
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Models.Main;
    using COLSTRAT.Service;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    public class MenuItemViewModel : INotifyPropertyChanged
    {
        #region Services
        private NavigationService navigationService;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region Attributes
        List<MenuItem> menuItems;
        ObservableCollection<MenuItem> _menuItems;
        #endregion

        public ObservableCollection<MenuItem> MenuItems
        {
            get { return _menuItems; }
            set
            {
                if (_menuItems != value)
                {
                    _menuItems = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MenuItems)));
                }
            }
        }

        #region Constructor
        public MenuItemViewModel()
        {
            LoadMenu();
        }
        #endregion


        #region Methods
        private void LoadMenu()
        {
            menuItems = new List<MenuItem>();
            menuItems.Add(new MenuItem
            {
                Icon = "dt-home",
                PageName = "Home",
                Title = Languages.Home
            });
            menuItems.Add(new MenuItem
            {
                Icon = "dt-person",
                PageName = "MyProfileView",
                Title = Languages.Profile,
            });
            menuItems.Add(new MenuItem
            {
                Icon = "dt-person-pin",
                PageName = "UbicationsView",
                Title = Languages.Ubication,
            });
            if (MainViewModel.GetInstante().CurrentCustomer.CustomerType.Equals(0))
            {
                menuItems.Add(new MenuItem
                {
                    Icon = "dt-refresh",
                    PageName = "SyncView",
                    Title = Languages.Sync_Data,
                });
            }
            menuItems.Add(new MenuItem
            {
                Icon = "dt-exit",
                PageName = "LogoutView",
                Title = Languages.btnExit
            });
            MenuItems = new ObservableCollection<MenuItem>(menuItems);
        }
        #endregion
        

    }
}
