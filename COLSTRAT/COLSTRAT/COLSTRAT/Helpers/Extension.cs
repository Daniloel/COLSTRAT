using System;
namespace COLSTRAT.Help
{
    public static class Util
    {
        //¿Es desarrollo?
        public static bool isDebug()
        {
            bool d = false;
#if DEBUG
            d = true;
#endif
            return d;
        }
        public static void log(string translate)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine(translate);
#endif
        }
        public static void log(string key, string translate)
        {
            log(key + ": " + translate);
        }
     /*   public static string trans(string translate)
        {
            string t = Resources.Resources.translate:
            if (t == "" || t == null || t.Length == 0)
            {
                t = translate;
            }
            return t;
        }


        public static string t(this string translate)
        {
            return trans(translate);
        }*/
        public static void debugTime(string tag, string step = "")
        {
            log("TIME." + tag + "." + step, DateTime.Now.ToString("hh.mm.ss.ffffff"));
        }

    }
}