using COLSTRAT.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLSTRAT.Service
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, Languages.Accept);
        }

        public async Task<bool> ShowConfirm(string title, string message)
        {
            return await App.Current.MainPage.DisplayAlert(title, message, Languages.Yes, Languages.Not);
        }
    }
}
