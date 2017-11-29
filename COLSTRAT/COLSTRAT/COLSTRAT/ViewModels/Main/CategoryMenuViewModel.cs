using COLSTRAT.Models;
using COLSTRAT.Service;
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
        List<Category> categories;
        string _titlePage;
        string description;
        ObservableCollection<Category> _categoryMenuItems;
        #endregion

        #region Properties
        public string TitlePage
        {
            get { return _titlePage; }
            set
            {
                if (_titlePage != value)
                {
                    _titlePage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitlePage)));
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
        public CategoryMenuViewModel(List<Category> categories,string description)
        {
            this.categories = categories;
            this.description = description;
            TitlePage = description;
            CategoryMenuItems = new ObservableCollection<Category>(categories.OrderBy(p => p.Name));
        }
        #endregion
    }
}
