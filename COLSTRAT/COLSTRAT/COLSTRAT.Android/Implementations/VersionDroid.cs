
[assembly: Xamarin.Forms.Dependency(typeof(COLSTRAT.Droid.Implementations.VersionDroid))]
namespace COLSTRAT.Droid.Implementations
{
    using Android.Content.PM;
    using COLSTRAT.Interfaces;
    using System;

    public class VersionDroid : IVersion
    {

        public string GetName()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            var name = manager.GetPackageInfo(context.PackageName, 0).ApplicationInfo.LoadLabel(context.PackageManager);
            return name;
        }

        public string GetVersion()
        {
            var context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public int GetBuild()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);
            return info.VersionCode;
        }

        public DateTime GetLastCompileDate()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            var a = manager.GetPackageInfo(context.PackageName, 0).LastUpdateTime;
            var d = JavaLongToDate(a);
            return d;
        }

        public DateTime JavaLongToDate(long javaLong)
        {
            DateTime unixYear0 = new DateTime(1970, 1, 1);
            long unixTimeStampInTicks = javaLong / 1000 * TimeSpan.TicksPerSecond;
            DateTime dtUnix = new DateTime(unixYear0.Ticks + unixTimeStampInTicks);
            return dtUnix;
        }
    }
}