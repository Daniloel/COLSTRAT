namespace COLSTRAT.Models
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels;
    using COLSTRAT.ViewModels.Main;
    using GalaSoft.MvvmLight.Command;
    using System;
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
            Category = new List<Category>();
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }
        private async void Edit()
        {
            MainViewModel.GetInstante().EditMenu = new EditMenuViewModel(this);
            await navigationService.Navigate("EditMenuView");
        }
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
        {
            var response = await dialogService.ShowConfirm(Languages.Warning, Languages.Message_Delete);
            if (!response)
                return;
            MainMenuViewModel.GetInstante().DeleteMenu(this);


        }
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
            mainViewModel.CategoryMenu = new CategoryMenuViewModel(Category);
            mainViewModel.CurrentMenu = this;
            await navigationService.Navigate("CategoryMenuView");

        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return MainMenuId;
        }
        #endregion
    }
}
