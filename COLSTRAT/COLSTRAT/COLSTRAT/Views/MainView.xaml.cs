using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COLSTRAT.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            
            btnIgneousRocks.Clicked += (sender,e) =>
            {  
                Navigation.PushAsync(new Rocks.IgneousView());
            };
            btnMetamorphicRocks.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Rocks.MetamorphicView());
            };
            btnSedimentaryRocks.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new Rocks.SedimentaryView());
            };
        }
    }
}