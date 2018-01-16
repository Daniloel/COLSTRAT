﻿namespace COLSTRAT.ViewModels
{
    using COLSTRAT.Service;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    public class MenuItemViewModel
    {
        #region Services
        private NavigationService navigationService;
        #endregion
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Constructor
        public MenuItemViewModel()
        {
            navigationService = new NavigationService();
        }
        #endregion


        #region Commands
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }
        async void Navigate()
        {
            await navigationService.Navigate(PageName);
        }
        #endregion

    }
}
