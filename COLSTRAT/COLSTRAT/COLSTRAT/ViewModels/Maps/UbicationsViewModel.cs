namespace COLSTRAT.ViewModels.Maps
{
    using COLSTRAT.Models.Maps;
    using Service;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;

    public class UbicationsViewModel
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Properties
        public ObservableCollection<Pin> Pins
        {
            get;
            set;
        }
        #endregion

        #region Contructor
        public UbicationsViewModel()
        {
            instance = this;
            dialogService = new DialogService();
            apiService = new ApiService();
        }
        #endregion
        #region Singleton
        static UbicationsViewModel instance;

        public static UbicationsViewModel GetInstance()
        {
            if (instance == null)
            {
                return new UbicationsViewModel();
            }

            return instance;
        }
        #endregion
        #region Methods

        public async Task LoadPins()
        {
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                await dialogService.ShowErrorMessage(con.Message);
                return;
            }
            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            var response = await apiService.GetList<Ubication>(
                urlBase,
                "/api",
                "/Ubications",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowErrorMessage(response.Message);
                return;
            }

            var ubications = (List<Ubication>)response.Result;
            Pins = new ObservableCollection<Pin>();
            foreach (var ubication in ubications)
            {
                Pins.Add(new Pin
                {
                    Address = ubication.Address,
                    Label = ubication.Description,
                    Position = new Position(ubication.Latitude,ubication.Longitude),
                    Type = PinType.Place
                });

            }
        }
        #endregion
    }
}
