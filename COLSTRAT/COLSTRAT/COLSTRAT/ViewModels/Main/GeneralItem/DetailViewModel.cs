namespace COLSTRAT.ViewModels.Main.GeneralItem
{
    using System.ComponentModel;
    using System.Windows.Input;
    using COLSTRAT.Models;
    using COLSTRAT.Service;
    using GalaSoft.MvvmLight.Command;
    public class DetailViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Services
        ImageService imageService;
        NavigationService navigationService;
        #endregion
        #region Attributes
        private string _name;
        private string _imageFullPath;
        private string _description;
        GeneralItem generalItem;
        Rock rock;
        #endregion

        #region Properties
        public string ImageFullPath
        {
            get { return _imageFullPath; }
            set
            {
                if (_imageFullPath != value)
                {
                    _imageFullPath = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageFullPath)));
                }
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }
        #endregion 
        
        #region Constructors
        public DetailViewModel(GeneralItem generalItem)
        {
            this.generalItem = generalItem;
            Name = generalItem.Name;
            Description = generalItem.Description;
            ImageFullPath = generalItem.ImageFullPath;
            navigationService = new NavigationService();
            imageService = new ImageService();
        }
        public DetailViewModel(Rock rock)
        {
            this.rock = rock;
            Name = rock.Name;
            Description = rock.Descripcion;
            ImageFullPath = rock.ImageFullPath;
            navigationService = new NavigationService();
            imageService = new ImageService();
        }
        #endregion

        #region Commands
        public ICommand ErrorImageCommand
        {
            get
            {
                return new RelayCommand(AlternativeImage);
            }
        }
        private void AlternativeImage()
        {
            if (imageService.ImageStatus.Equals(ImageService.GetImageStatus.NotAvailable))
            {
                return;
            }
            if (imageService.ImageStatus.Equals(ImageService.GetImageStatus.First))
            {
                if (generalItem != null)
                    ImageFullPath = imageService.getAlternativeUrl(generalItem.Image);
                if (rock != null)
                    ImageFullPath = imageService.getAlternativeUrl(rock.Image);
            }
            else if (imageService.ImageStatus.Equals(ImageService.GetImageStatus.Alternative))
            {
                ImageFullPath = imageService.ContentNotAvailable;
                imageService.ImageStatus = ImageService.GetImageStatus.NotAvailable;
            }
        }
        #endregion
    }
}
