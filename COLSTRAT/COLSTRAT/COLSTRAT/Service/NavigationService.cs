namespace COLSTRAT.Service
{
    using COLSTRAT.Views.Rocks;
    using System.Threading.Tasks;
    using Views;
    using ViewModels;
    using Xamarin.Forms;

    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            
            Application.Current.MainPage = new MainView();
               
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
                default:
                    break;
            }
        }
    }
}

