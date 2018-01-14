using COLSTRAT.Service;
using COLSTRAT.ViewModels;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows.Input;

namespace COLSTRAT.Models
{
    public class Category
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

        #region Commands
        public ICommand OpenCategoryCommand
        {
            get
            {
                return new RelayCommand(OpenCategoryDetail);
            }
        }

        private async void OpenCategoryDetail()
        {
            await dialogService.ShowMessage("Hello", "Pronto añadiremos el show");
        }
        #endregion
    }
}
