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
        public async Task ShowErrorMessage(string message)
        {
            await App.Current.MainPage.DisplayAlert(Languages.Warning, message, Languages.Accept);
        }
        public async Task ShowMessage(string message)
        {
            await App.Current.MainPage.DisplayAlert(Languages.Message_Title, message, Languages.Accept);
        }
        public async Task<string> ShowImageOptions()
        {
            return await App.Current.MainPage.DisplayActionSheet(
                Languages.Option_Pick_Photo,
                Languages.Cancel,
                null,
                Languages.From_Gallery,
                Languages.From_Camera);
        }
    }
}
