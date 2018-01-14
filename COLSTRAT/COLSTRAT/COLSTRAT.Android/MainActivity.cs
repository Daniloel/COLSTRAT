
using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading;

namespace COLSTRAT.Droid
{
    [Activity(Label = "COLSTRAT App", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            //Plugin.Iconize.Iconize.With(new COLSTRATIconsModule());

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule())
                          .With(new Plugin.Iconize.Fonts.MaterialModule());

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
            
            var config = new FFImageLoading.Config.Configuration()
            {
                VerboseLogging = false,
                VerbosePerformanceLogging = false,
                VerboseMemoryCacheLogging = false,
                VerboseLoadingCancelledLogging = false,
                //Logger = new CustomLogger(),
            };
            ImageService.Instance.Initialize(config);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);
            LoadApplication(new App());
        }
        
    }
}

