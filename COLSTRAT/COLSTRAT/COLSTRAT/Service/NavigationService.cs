namespace COLSTRAT.Service
{
    using COLSTRAT.Views.Rocks;
    using System.Threading.Tasks;
    using Views;
    using Xamarin.Forms;
    using COLSTRAT.Views.Main;
    using COLSTRAT.Views.Main.GeneralItem;
    using COLSTRAT.Views.Login;
    using COLSTRAT.Views.Main.MainMenu;
    using COLSTRAT.Resources;
    using COLSTRAT.Views.Maps;
    using COLSTRAT.Helpers;

    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView()) { BarBackgroundColor = Colors.MainColor };
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "Home":
                    SetMainPage("MasterView");
                    break;
                case "MainMenuView":
                    await App.Navigator.PushAsync(new MainMenuView());
                    break;
                case "RocksView":
                    await App.Navigator.PushAsync(new RocksView());
                    break;
                case "CategoryMenuView":
                    await App.Navigator.PushAsync(new CategoryMenuView());
                    break;
                case "NewCategoryView":
                    await App.Navigator.PushAsync(new NewCategoryView());
                    break;
                case "NewMenuView":
                    await App.Navigator.PushAsync(new NewMenuView());
                    break;
                case "EditCategoryView":
                    await App.Navigator.PushAsync(new EditCategoryView());
                    break;
                case "EditMenuView":
                    await App.Navigator.PushAsync(new EditMenuView());
                    break;
                case "GeneralItemView":
                    await App.Navigator.PushAsync(new GeneralItemView());
                    break;
                case "NewGeneralItemView":
                    await App.Navigator.PushAsync(new NewGeneralItemView());
                    break;
                case "EditGeneralItemView":
                    await App.Navigator.PushAsync(new EditGeneralItemView());
                    break;
                case "RocksMenuView":
                    await App.Navigator.PushAsync(new RocksMenuView());
                    break;
                case "NewCustomerView":
                    await App.Navigator.PushAsync(new NewCustomerView());
                    break;
                case "LogoutView":
                    DialogService dialog = new DialogService();
                    var response = await dialog.ShowConfirm(Languages.Warning, Languages.Confirm_Exit);
                    if (response)
                        SetMainPage("LoginView");
                    break;
                case "UbicationsView":
                    await App.Navigator.PushAsync(new UbicationsView());
                    break;
                default:
                    break;
            }
        }
        public async Task NavigateOnLogin(string pageName)
        {
            switch (pageName)
            {
                case "NewCustomerView":
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new NewCustomerView());
                    break;
              /*  case "LoginFacebookView":
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new LoginFacebookView());
                    break;
                case "PasswordRecoveryView":
                    await Application.Current.MainPage.Navigation.PushAsync(
                        new PasswordRecoveryView());
                    break;*/
            }
        }
        public async Task BackOnLogin()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public async Task Back()
        {
            await App.Navigator.PopAsync();
        }
    }
}

