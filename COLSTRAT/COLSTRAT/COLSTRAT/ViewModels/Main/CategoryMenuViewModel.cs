using COLSTRAT.Helpers;
using COLSTRAT.Models;
using COLSTRAT.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace COLSTRAT.ViewModels.Main
{
    public class CategoryMenuViewModel : INotifyPropertyChanged
    {

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion
        #region Attributes
        bool _isRefreshing;
        List<Category> categories;
        ObservableCollection<Category> _categoryMenuItems;
        #endregion

        #region Properties
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                if (_isRefreshing != value)
                {
                    _isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
        public ObservableCollection<Category> CategoryMenuItems
        {
            get { return _categoryMenuItems; }
            set
            {
                if (_categoryMenuItems != value)
                {
                    _categoryMenuItems = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CategoryMenuItems)));
                }
            }
        }
        #endregion

        #region Constructor
        public CategoryMenuViewModel(List<Category> categories)
        {
            instance = this;
            if (categories != null)
            {
                this.categories = categories;
                CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(p => p.Name));
            }
        }
        #endregion


        #region Singleton
        static CategoryMenuViewModel instance;

        public static CategoryMenuViewModel GetInstante()
        {
            if (instance == null)
            {
                return new CategoryMenuViewModel(new List<Category>());
            }

            return instance;
        }

        public void AddMenu(Category category)
        {
            IsRefreshing = true;
            categories.Add(category);
            CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        public void UpdateMenu(Category category)
        {
            IsRefreshing = true;
            var oldCategory = categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();
            oldCategory = category;
            CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        public async void DeleteCategory(Category category)
        {
            IsRefreshing = true;
            apiService = new ApiService();
            dialogService = new DialogService();
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                IsRefreshing = false;
                await dialogService.ShowErrorMessage(con.Message);
                return;
            }

            string urlBase = Application.Current.Resources["URL_API"].ToString();
            var mainViewModel = MainViewModel.GetInstante();
            var response = await apiService.Delete(
                urlBase,
                "/api",
                "/Categories",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                category);

            if (!response.IsSuccess)
            {
                IsRefreshing = false;
                if (response.Message == "mdg4ymQsXUPdMYLR74DMSqdwMdppHC1yssL5+SuIvJ8B3a7Pf2PIBULCV1+0oQEXewaNRYU09w76N1tktNaPxQ==")
                {
                    await dialogService.ShowErrorMessage(Languages.Error_Record_Relateds);
                }
                else
                {
                    await dialogService.ShowErrorMessage(Languages.ErrorResponseNotFound);
                }
                return;
            }
            categories.Remove(category);

            CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
            IsRefreshing = false;
        }
        #endregion

    }
}
