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
    using COLSTRAT.Helpers;

    public class LoginFacebookRenderer : PageRenderer
    {
        public LoginFacebookRenderer()
        {
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "908787362612965",
                scope: "",
                authorizeUrl: new Uri("https://www.facebook.com/v2.11/dialog/oauth"),
                redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            auth.Title = Languages.Login_With_FB;
            auth.AllowCancel = true;
            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var profile = await GetFacebookProfileAsync(accessToken);
                    App.LoginFacebookSuccess(profile);
                }
                else
                {
                    App.LoginFacebookFail();
                }
            };
            activity.StartActivity(auth.GetUI(activity));
        }

        async Task<FacebookResponse> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl = "https://graph.facebook.com/v2.11/me?fields=name,picture.width(999),cover,age_range,devices,email,gender,is_verified,birthday,languages,work,website,religion,location,locale,link,first_name,last_name,hometown&access_token=" + accessToken;
            var httpClient = new HttpClient();
            FacebookResponse facebookResponse = new FacebookResponse();
            try
            {
                var userJson = await httpClient.GetStringAsync(requestUrl);

                facebookResponse = JsonConvert.DeserializeObject<FacebookResponse>(userJson);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return facebookResponse;
        }
    }
}