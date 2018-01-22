using COLSTRAT.Helpers;
using COLSTRAT.Service;
using COLSTRAT.ViewModels;
using COLSTRAT.ViewModels.Main.GeneralItem;
using GalaSoft.MvvmLight.Command;
using Plugin.Connectivity;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace COLSTRAT.Models
{
    public class GeneralItem : INotifyPropertyChanged
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        ImageService imageService;
        #endregion

        #region Attributes
        string _imageFullPath;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Propeties
        public int CategoryId { get; set; }
        public int GeneralItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public byte[] ImageArray { get; set; } 

        
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    _imageFullPath = imageService.ContentNotAvailable;
                    imageService.ImageStatus = ImageService.GetImageStatus.NotAvailable;
                }
                if (imageService.ImageStatus.Equals(ImageService.GetImageStatus.NoHasSet) && (!string.IsNullOrEmpty(Image)))
                {
                    _imageFullPath = imageService.getURL(Image);
                }
                return _imageFullPath;
            }
            set
            {
                if (_imageFullPath != value)
                {
                    _imageFullPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageFullPath)));
                }
            }
        }
        #endregion

        #region Contructor
        public GeneralItem()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
            imageService = new ImageService();
        }
        #endregion


        #region Methods
        public override int GetHashCode()
        {
            return GeneralItemId;
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
                ImageFullPath = imageService.getAlternativeUrl(Image);
            }
            else if (imageService.ImageStatus.Equals(ImageService.GetImageStatus.Alternative))
            {
                ImageFullPath = imageService.ContentNotAvailable;
                imageService.ImageStatus = ImageService.GetImageStatus.NotAvailable;
            }
        }
        public ICommand EditCommand
        {
            get
            {
                return new RelayCommand(Edit);
            }
        }
        private async void Edit()
        {
            MainViewModel.GetInstante().EditGeneralItem = new EditGeneralItemViewModel(this);
            await navigationService.Navigate("EditGeneralItemView");
        }
        
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
        {
            var response = await dialogService.ShowConfirm(Languages.Warning, Languages.Message_Delete);
            if (!response)
                return;
            GeneralItemViewModel.GetInstante().DeleteCategory(this);
        }
        #endregion
    }
}
