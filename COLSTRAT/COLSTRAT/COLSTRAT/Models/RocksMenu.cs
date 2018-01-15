namespace COLSTRAT.Models
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels;
    using COLSTRAT.ViewModels.Rocks;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class RocksMenu
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        public int RocksMenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Rock> Rocks { get; set; }

        #region Contructor
        public RocksMenu()
        {
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
            
        }
        public ICommand OpenRockCommand
        {
            get
            {
                return new RelayCommand(OpenRockDetail);
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
            RocksMenuViewModel.GetInstante().DeleteCategory(this);
        }

        private async void OpenRockDetail()
        {
            
            var mainViewModel = MainViewModel.GetInstante();
            mainViewModel.Rocks = new RocksViewModel(Rocks);
            await navigationService.Navigate("RocksView");

        }
        #endregion
    }
}