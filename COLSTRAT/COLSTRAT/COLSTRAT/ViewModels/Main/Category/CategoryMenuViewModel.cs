using COLSTRAT.Helpers;
using COLSTRAT.Models;
using COLSTRAT.Service;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace COLSTRAT.ViewModels.Main
{
    public class CategoryMenuViewModel : INotifyPropertyChanged
    {

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Services
        DataService dataService;
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
                SaveCategoriesOnDB();
                CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(p => p.Name));
            }
            dataService = new DataService();
            apiService = new ApiService();
            dialogService = new DialogService();
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
        #region Methods
        private async void LoadCategoryMenu()
        {
            IsRefreshing = true;
            var con = await apiService.CheckConnection();
            if (!con.IsSuccess)
            {
                categories = dataService.Get<Category>(true).Where(p => p.MainMenu == MainViewModel.GetInstante().CurrentMenu).ToList();
                if (categories.Count == 0)
                {
                    IsRefreshing = false;
                    await dialogService.ShowErrorMessage(Languages.Message_Not_Data);
                    return;
                }
                CategoryMenuItems = new ObservableCollection<Category>(categories.
                    OrderBy(c => c.Name));
            }
            else{
                string urlBase = Application.Current.Resources["URL_API"].ToString();
                var mainViewModel = MainViewModel.GetInstante();
                var response = await apiService.GetList<Category>(
                    urlBase,
                    "/api",
                    "/MainMenus",
                    mainViewModel.Token.TokenType,
                    mainViewModel.Token.AccessToken,
                    mainViewModel.CurrentMenu.MainMenuId);

                if (!response.IsSuccess)
                {
                    IsRefreshing = false;
                    await dialogService.ShowErrorMessage(response.Message);
                    return;
                }
                categories = (List<Category>)response.Result;
                SaveCategoriesOnDB();
                CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(c => c.Name));
            }
            IsRefreshing = false;
        }

        private void SaveCategoriesOnDB()
        {
            dataService.DeleteAll<Category>();
            foreach (var category in categories)
            {
                dataService.Insert(category);
                dataService.Save(category.GeneralItems);
                dataService.Save(category.RocksMenu);
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadCategoryMenu);
            }
        }
        #endregion

    }
}
