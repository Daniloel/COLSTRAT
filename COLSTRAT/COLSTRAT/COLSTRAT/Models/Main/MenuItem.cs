using COLSTRAT.Helpers;
using COLSTRAT.Service;
using COLSTRAT.ViewModels;
using GalaSoft.MvvmLight.Command;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace COLSTRAT.Models.Main
{
    public class MenuItem
    {
        #region Services
        NavigationService navigationService;
        DataService dataService;
        ApiService apiService;
        DialogService dialog;
        #endregion
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Constructor
        public MenuItem()
        {
            dialog = new DialogService();
            dataService = new DataService();
            navigationService = new NavigationService();
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand { get { return new RelayCommand(Navigate); } }
        async void Navigate()
        {
            switch (PageName)
            {
                case "Home":
                    navigationService.SetMainPage("MasterView");
                    break;
                case "LogoutView":
                    var response = await dialog.ShowConfirm(Languages.Warning, Languages.Confirm_Exit);
                    if (response)
                    {
                        var mainViewModel = MainViewModel.GetInstante();
                        mainViewModel.Token.IsRemembered = false;
                        dataService.Update(mainViewModel.Token);
                        dataService.DeleteAll<Customer>();
                        mainViewModel.Login = new ViewModels.Login.LoginViewModel();
                        navigationService.SetMainPage("LoginView");
                    }
                    break;
                case "UbicationsView":
                    MainViewModel.GetInstante().Ubications = new ViewModels.Maps.UbicationsViewModel();
                    await navigationService.Navigate(PageName);
                    break;
                case "SyncView":
                    MainViewModel.GetInstante().Sync = new ViewModels.Sync.SyncViewModel();
                    await navigationService.Navigate(PageName);
                    break;
                case "MyProfileView":
                    MainViewModel.GetInstante().MyProfile = new ViewModels.Profile.MyProfileViewModel();
                    await navigationService.Navigate(PageName);
                    break;
            }
            
        }
        #endregion
    }
}
