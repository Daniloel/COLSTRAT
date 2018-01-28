namespace COLSTRAT.Views.Main.MainMenu
{
    using COLSTRAT.Helpers;
    using COLSTRAT.Models;
    using COLSTRAT.Service;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenuView : ContentPage
    {
        public MainMenuView()
        {
            InitializeComponent();
            SessionService session = new SessionService();
            Customer user = session.GetCurrentUser();
            if (user != null && user.CustomerType == 0)
            {
                var toolbar = new ToolbarItem
                {
                    Icon = "plus_circle.png"
                };
                toolbar.SetBinding(ToolbarItem.CommandProperty, "NewMenuCommand");
                ToolbarItems.Add(toolbar);
            }
            Lv.ItemTemplate = new DataTemplate(typeof(MenuItemCell));
        }
    }
}