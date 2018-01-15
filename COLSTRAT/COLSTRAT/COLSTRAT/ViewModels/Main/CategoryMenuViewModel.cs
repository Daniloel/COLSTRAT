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

        internal void AddMenu(Category category)
        {
            categories.Add(category);
            CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(c => c.Description));
        }
        #endregion

    }
}
