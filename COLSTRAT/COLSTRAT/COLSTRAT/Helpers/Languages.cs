using System;
using System.Collections.Generic;
namespace COLSTRAT.Helpers
{
    using Xamarin.Forms;
    using Interfaces;
    using Resources;

    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resources.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }
        public static string Message_Image_Content
        {
            get { return Resources.message_image_content; }
        }
        public static string Message_Image
        {
            get { return Resources.message_image; }
        }
        public static string Message_Not_Select_Rock
        {
            get { return Resources.message_not_select_rock; }
        }
        public static string Yes
        {
            get { return Resources.yes; }
        }
        public static string Not
        {
            get { return Resources.not; }
        }
        public static string Accept
        {
            get { return Resources.accept; }
        }
        public static string Warning
        {
            get { return Resources.warning; }
        }
        public static string Welcome
        {
            get { return Resources.welcome; }
        }
        public static string SedimentaryRocks
        {
            get { return Resources.SedimentaryRocks; }
        }
        public static string MetamorphicRocks
        {
            get { return Resources.MetamorphicRocks; }
        }
        public static string IgneousRocks
        {
            get { return Resources.IgneousRocks; }
        }
        public static string pSelectRock
        {
            get { return Resources.picker_select_the_rock; }
        }
        public static string lName
        {
            get { return Resources.label_name; }
        }
        public static string lDescription
        {
            get { return Resources.label_description; }
        }
        public static string lMinerals
        {
            get { return Resources.label_minerals; }
        }
        public static string lImage
        {
            get { return Resources.label_image; }
        }

        public static string btnShow
        {
            get { return Resources.btnShow; }
        }
        public static string btnExit
        {
            get { return Resources.btnExit; }
        }
    }
}
