namespace COLSTRAT.Models
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Service;
    using COLSTRAT.ViewModels;
    using COLSTRAT.ViewModels.Rocks;
    using GalaSoft.MvvmLight.Command;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
    using System.Collections.Generic;
    using System.Windows.Input;

    public class RocksMenu
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Properties
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }
        [PrimaryKey]
        public int RocksMenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ManyToOne]
        public Category Category { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Rock> Rocks { get; set; }
        #endregion
        #region Methods
        public override int GetHashCode()
        {
            return RocksMenuId;
        }
        #endregion
        #region Contructor
        public RocksMenu()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        private async void OpenRockDetail()
        {
            
            var mainViewModel = MainViewModel.GetInstante();
            mainViewModel.Rocks = new RocksViewModel(Rocks);
            await navigationService.Navigate("RocksView");

        }
        #endregion
    }
}