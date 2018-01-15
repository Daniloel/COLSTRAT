using COLSTRAT.Helpers;
using COLSTRAT.Service;
using COLSTRAT.ViewModels.Rocks;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace COLSTRAT.Models
{
    public class Rock
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
                    return "noimage";
                }
                return string.Format("http://colstrat.somee.com{0}", Image.Trim('~'));
            }
        }
        DialogService dialogService;
        public Rock()
        {
            dialogService = new DialogService();
        }
        public override int GetHashCode()
        {
            return RockId;
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
