namespace COLSTRAT.Models
{
    using COLSTRAT.Views.Rocks;
    using COLSTRAT.ViewModels;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Xamarin.Forms;
    using COLSTRAT.ViewModels.Rocks;
    using COLSTRAT.Service;

    public class TypeOfRock
    {
        #region SErvices
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        public int TypeOfRockId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Rock> Rocks { get; set; }

        #region Constructors
        public TypeOfRock()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion
        #region Commands
        public ICommand OpenTypeOfRockCommand
        {
            get
            {
                return new RelayCommand(OpenTypeOfRock);
            }
        }

        private async void OpenTypeOfRock()
        {
            var mainViewModel = MainViewModel.GetInstante();
            mainViewModel.Rocks = new RocksViewModel(Rocks);
            await navigationService.Navigate("RocksView");
            
        }
        #endregion
    }
}
