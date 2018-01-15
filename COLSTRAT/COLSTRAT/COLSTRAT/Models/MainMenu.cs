namespace COLSTRAT.Models
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels;
    using COLSTRAT.ViewModels.Main;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MainMenu
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Attributes
        public List<OptionKeyValue> listColors;
        public string Icon { get; set; }
        public int MainMenuId { get; set; }
        public string Description { get; set; }
        public List<Category> Category { get; set; }
        public string ColorChanged
        {
            get
            {
                return GetColor();
            }
        }
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
        string GetColor()
        {
            var color = listColors.FirstOrDefault(c => (bool)c.val == false);
            color.val = true;
            return color.key;
        }

        void CreateColors()
        {
            Random r = new Random();
            foreach (var item in Category)
            {
                Color c = Color.FromRgb(r.Next(0, 256),r.Next(0, 256), r.Next(0, 256));
                while (listColors.Any(co => co.key == c.ToString()))
                {
                    c = Color.FromRgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                }
                listColors.Add(new OptionKeyValue(c.ToString(),false));
            }
        }
    }
}
