using COLSTRAT.Models;
using COLSTRAT.ViewModels;
using System.Linq;

namespace COLSTRAT.Service
{
    public class SessionService
    {
        DataService dataService;
        public SessionService()
        {
            dataService = new DataService();
        }
        public Customer GetCurrentUser()
        {
            if (MainViewModel.GetInstante().CurrentCustomer != null)
                return MainViewModel.GetInstante().CurrentCustomer;

            return dataService.First<Customer>(false);
        }
    }
}
