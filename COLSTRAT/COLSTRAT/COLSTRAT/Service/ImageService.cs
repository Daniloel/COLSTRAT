namespace COLSTRAT.Service
{
    using System;
    using System.Collections.Generic;

    public class ImageService
    {
        #region Enum
        public enum GetImageStatus
        {
            NoHasSet = 0,
            First = 100,
            Alternative = 200,
            ServerExtern1 = 300,
            ServerExtern2 = 400,
            NotAvailable = 404
        }
        #endregion
        #region Attributes
        string TAG = "ImageService";
        public GetImageStatus ImageStatus = GetImageStatus.NoHasSet;
        public string ContentNotAvailable = "http://colstrat-api.somee.com/Content/no-image/no-image.png";
        private static string ContentImgAPI = "http://colstrat-api.somee.com";
        private static string ContentImgBackend = "http://colstrat.somee.com";
        List<String> servers;
        #endregion
        #region Contructor
        public ImageService()
        {
            servers = new List<string>();
            servers.Add(ContentImgAPI);
            servers.Add(ContentImgBackend);
        }
        #endregion
        #region Methods

        public String getURL(string resource)
        {
            String url = "";
            url = buildUrl(servers[0], resource);
            ImageStatus = GetImageStatus.First;
            return url;
        }
        
        public String getAlternativeUrl(string resource)
        {
            String url = "";
            url = buildUrl(servers[1], resource);
            ImageStatus = GetImageStatus.Alternative;
            return url;
        }

        private String buildUrl(string baseUrl, string path)
        {
            String url = string.Format(baseUrl + "{0}", path.Trim('~'));
            return url;
        } 
        #endregion
    }
    
}

