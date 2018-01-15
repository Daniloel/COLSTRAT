namespace COLSTRAT.Service
{
    using COLSTRAT.Views.Rocks;
    using System.Threading.Tasks;
    using Views;
    using Xamarin.Forms;
    using COLSTRAT.Views.Main;
    using System;

    public class NavigationService
    {
        public void SetMainPage(string pageName = null)
        {
            if (pageName is default)
                Application.Current.MainPage = new MasterView();
               
        }

        public async Task Navigate(string pageName)
        {
            App.Master.IsPresented = false;

            switch (pageName)
            {
                case "IgneousView":
                    await App.Navigator.PushAsync(new IgneousView());
                    break;
                case "MetamorphicView":
                    await App.Navigator.PushAsync(new MetamorphicView());
                    break;
                case "SedimentaryView":
                    await App.Navigator.PushAsync(new SedimentaryView());
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
                default:
                    break;
            }
        }

        public async Task Back()
        {
            await App.Navigator.PopAsync();
        }
    }
}

