using COLSTRAT.Helpers;
using COLSTRAT.Service;
using COLSTRAT.ViewModels;
using COLSTRAT.ViewModels.Main.GeneralItem;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace COLSTRAT.Models
{
    public class GeneralItem
    {
        #region Services
        DialogService dialogService;
        NavigationService navigationService;
        #endregion

        #region Propeties
        public int CategoryId { get; set; }
        public int GeneralItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                {
                    return "http://colstrat-api.somee.com/Content/no-image/no-image.png";
                }
                return string.Format("http://colstrat.somee.com{0}", Image.Trim('~'));
            }
        }
        #endregion

        #region Contructor
        public GeneralItem()
        {
            dialogService = new DialogService();
            navigationService = new NavigationService();
        }
        #endregion

        public override int GetHashCode()
        {
            return GeneralItemId;
        }

        #region Commands
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
