[assembly: Xamarin.Forms.Dependency(typeof(COLSTRAT.iOS.Implementations.VersioniOS))]
namespace COLSTRAT.iOS.Implementations
{
    using System;
    using Foundation;
    using COLSTRAT.Interfaces;
    public class VersioniOS : IVersion
    {
        public string GetName()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleDisplayName").ToString();
        }
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public int GetBuild()
        {
            return int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString());
        }

        public DateTime GetLastCompileDate()
        {
            throw new NotImplementedException();
        }
    }
}