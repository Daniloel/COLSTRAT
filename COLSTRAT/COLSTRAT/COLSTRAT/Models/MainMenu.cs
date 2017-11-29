namespace COLSTRAT.Models
{
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels;
    using COLSTRAT.ViewModels.Main;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class MainMenu
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Attributes
        public int MainMenuId { get; set; }
        public string Description { get; set; }
        public List<Category> Category { get; set; } 
        #endregion

        #region Contructor
        public MainMenu()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand OpenDetailCommand
        {
            get
            {
                return new RelayCommand(OpenDetail);
            }
        }

        private async void OpenDetail()
        {
            var mainViewModel = MainViewModel.GetInstante();
            mainViewModel.CategoryMenu = new CategoryMenuViewModel(Category,Description);
            await navigationService.Navigate("CategoryMenuView");

        }
        #endregion
    }
}
