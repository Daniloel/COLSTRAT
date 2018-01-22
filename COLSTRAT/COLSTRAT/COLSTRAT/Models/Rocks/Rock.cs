using COLSTRAT.Helpers;
using COLSTRAT.Service;
using COLSTRAT.ViewModels.Rocks;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace COLSTRAT.Models
{
    public class Rock : INotifyPropertyChanged
    {
        public int RockId { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public string Descripcion { get; set; }

        public string Minerals_Composition { get; set; }

        public string UseFor { get; set; }

        public string Structure { get; set; }

        public string Chemical_Composition { get; set; }

        public string Mechanical_Strength { get; set; }

        public string Porosity { get; set; }

        public int MohsScaleId { get; set; }

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
        string _imageFullPath;
        ImageService imageService;
        DialogService dialogService;

        public event PropertyChangedEventHandler PropertyChanged;

        public Rock()
        {
            dialogService = new DialogService();
            imageService = new ImageService();
        }
        public override int GetHashCode()
        {
            return RockId;
        }
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
            RocksViewModel.GetInstante().DeleteCategory(this);
        }
    }
}
