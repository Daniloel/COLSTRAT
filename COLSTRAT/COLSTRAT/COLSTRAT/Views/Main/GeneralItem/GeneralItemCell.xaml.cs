using COLSTRAT.Helpers;
using COLSTRAT.Models;
using COLSTRAT.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COLSTRAT.Views.Main.GeneralItem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeneralItemCell : ViewCell
    {
        public GeneralItemCell()
        {
            InitializeComponent();
            SessionService session = new SessionService();
            Customer user = session.GetCurrentUser();
            if (user != null && user.CustomerType == 0)
            {
                MenuItem i1 = new MenuItem
                {
                    Text = Languages.Edit
                };
                i1.SetBinding(MenuItem.CommandProperty, "EditCommand");
                MenuItem i2 = new MenuItem
                {
                    IsDestructive = true,
                    Text = Languages.Delete
                };
                i2.SetBinding(MenuItem.CommandProperty, "DeleteCommand");
                ContextActions.Add(i1);
                ContextActions.Add(i2);
            }
        }
    }
}