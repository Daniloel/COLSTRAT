using COLSTRAT.Models;
using COLSTRAT.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COLSTRAT.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryMenuView : ContentPage
    {
        public CategoryMenuView()
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
                toolbar.SetBinding(ToolbarItem.CommandProperty, "NewCategoryCommand");
                ToolbarItems.Add(toolbar);
            }
            Lv.ItemTemplate = new DataTemplate(typeof(Category.CategoryCell));
        }
    }
}