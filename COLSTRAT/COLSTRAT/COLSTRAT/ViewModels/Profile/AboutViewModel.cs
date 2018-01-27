using COLSTRAT.Interfaces;
using COLSTRAT.Service;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace COLSTRAT.ViewModels.Profile
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        #region Attributes
        string _name;
        string _version;
        string _compilation;
        string _imageSource;
        string _copyright;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }
        public string Version
        {
            get { return _version; }
            set
            {
                if (_version != value)
                {
                    _version = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Version)));
                }
            }
        }
        public string Compilation
        {
            get { return _compilation; }
            set
            {
                if (_compilation != value)
                {
                    _compilation = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Compilation)));
                }
            }
        }
        public string ImageSource
        {
            get { return _imageSource; }
            set
            {
                if (_imageSource != value)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }
        }
        public string Copyright
        {
            get { return _copyright; }
            set
            {
                if (_copyright != value)
                {
                    _copyright = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Copyright)));
                }
            }
        }
        #endregion
        #region Contructor
        public AboutViewModel()
        {
            SetVersionInfo();
            SetLogo();
            SetCopyright();
        }
        #endregion
        #region Methods
        void SetVersionInfo()
        {
            Name = DependencyService.Get<IVersion>().GetName();
            Version = "Versión " + DependencyService.Get<IVersion>().GetVersion();
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                Compilation = DependencyService.Get<IVersion>().GetLastCompileDate().ToString();
            }
        }


        void SetLogo()
        {
            ImageSource = ImageService.logoSingle;
        }
        private void SetCopyright()
        {
            Copyright = string.Format("{0}{1} COLSTRAT By Daniel Tovar.{2}Todos los derechos reservados", "\u00a9", DateTime.Now.Year, "\n");

        }
        #endregion
    }
}
