using COLSTRAT.Helpers;
using COLSTRAT.Service;
using COLSTRAT.ViewModels;
using COLSTRAT.ViewModels.Rocks;
using GalaSoft.MvvmLight.Command;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace COLSTRAT.Models
{
    public class Rock : INotifyPropertyChanged
    {
        #region Attributes
        string _imageFullPath;
        ImageService imageService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Properties
        [ForeignKey(typeof(RocksMenu))]
        public int RocksMenuId { get; set; }
        [PrimaryKey]
        public int RockId { get; set; }
        [ManyToOne]
        public RocksMenu RocksMenu { get; set; }
        public string Image { get; set; }

        public string Name { get; set; }

        public bool PendingToSave { get; set; }
        public string Descripcion { get; set; }

        public string Minerals_Composition { get; set; }

        public string UseFor { get; set; }

        public string Structure { get; set; }

        public string Chemical_Composition { get; set; }

        public string Mechanical_Strength { get; set; }

        public string Porosity { get; set; }

        public int MohsScaleId { get; set; }
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


        #region Events
        public event PropertyChangedEventHandler PropertyChanged; 
        #endregion

        #region Contructor
        public Rock()
        {
            navigationService = new NavigationService();
            dialogService = new DialogService();
            imageService = new ImageService();
        } 
        #endregion
        #region Methods
        public override int GetHashCode()
        {
            return RockId;
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
        public ICommand OpenDetailRockCommand
        {
            get
            {
                return new RelayCommand(OpenDetailRock);
            }
        }

        private void OpenDetailRock()
        {
            
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
            MainViewModel.GetInstante().EditRock = new EditRockViewModel(this);
            await navigationService.Navigate("EditRockView");
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
        #endregion
    }
}
