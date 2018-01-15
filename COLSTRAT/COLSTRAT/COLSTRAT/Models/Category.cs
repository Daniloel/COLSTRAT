﻿using COLSTRAT.Helpers;
using COLSTRAT.Service;
using COLSTRAT.ViewModels;
using COLSTRAT.ViewModels.Main;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace COLSTRAT.Models
{
    public class Category : MainMenu
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Attributes
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int GeologyCategoryId { get; set; }
        public List<RocksMenu> RocksMenu { get; set; }

        public int FluidsCategoryId { get; set; }
        public List<Valvule> Valvules { get; set; } 
        #endregion

        #region Contructor
        public Category()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion
        #region Methods
        public override int GetHashCode()
        {
            return CategoryId;
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
            MainViewModel.GetInstante().EditCategory = new EditCategoryViewModel(this);
            await navigationService.Navigate("EditCategoryView");
        }
        public ICommand OpenCategoryCommand
        {
            get
            {
                return new RelayCommand(OpenCategoryDetail);
            }
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
            CategoryMenuViewModel.GetInstante().DeleteCategory(this);
        }

        private async void OpenCategoryDetail()
        {
            await dialogService.ShowMessage("Hello", "Pronto añadiremos el show");
        }
        #endregion
    }
}
