﻿namespace COLSTRAT.Models
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels;
    using COLSTRAT.ViewModels.Main;
    using GalaSoft.MvvmLight.Command;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
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
        public List<Color> listColors;
        public string Icon { get; set; }
        public string IconFind
        {
            get
            {
                if (string.IsNullOrEmpty(Icon))
                    return "dt-broken-image";

                return Icon;
            }
        }
        [PrimaryKey]
        public int MainMenuId { get; set; }
        public string Description { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Category> Category { get; set; }
        public Color ColorChanged
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
            listColors = new List<Color>();
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
        Color GetColor()
        {
            Random r = new Random();

            Color c = Color.FromRgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            while (listColors.Any(co => co == c))
            {
                c = Color.FromRgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
            }
            listColors.Add(c);
            return c;
        }
    }
}
