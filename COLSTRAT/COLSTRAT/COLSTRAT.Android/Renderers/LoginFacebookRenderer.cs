using Xamarin.Forms;
using COLSTRAT.Views.Login;

[assembly: ExportRenderer(typeof(LoginFacebookView),typeof(COLSTRAT.Droid.Renderers.LoginFacebookRenderer))]
namespace COLSTRAT.Droid.Renderers
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Android.App;
    using COLSTRAT.Models.Login;
    using Newtonsoft.Json;
    using Xamarin.Auth;
    using Xamarin.Forms.Platform.Android;

    public class LoginFacebookRenderer : PageRenderer
    {
        public LoginFacebookRenderer()
        {
            var activity = this.Context as Activity;
        }
    }
}