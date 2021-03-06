﻿using COLSTRAT.Helpers;
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

namespace COLSTRAT.ViewModels.Rocks
{
    public class RocksViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
        #region Attributes
        List<Rock> rocks;
        ObservableCollection<Rock> _rocks;
        bool _isRefreshing;
        string _filter; string _labelInfo;
        bool _hasData;
        #endregion
        #region Properties
        public string LabelInfo
        {
            get { return _labelInfo; }
            set
            {
                if (_labelInfo != value)
                {
                    _labelInfo = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LabelInfo)));
                }
            }
        }
        public bool HasData
        {
            get { return _hasData; }
            set
            {
                if (_hasData != value)
                {
                    _hasData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HasData)));
                }
            }
        }
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    SearchRock();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
                }
            }
        }
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
        public ObservableCollection<Rock> Rocks
        {
            get { return _rocks; }
            set
            {
                if (_rocks != value)
                {
                    _rocks = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Rocks)));
                }
            }
        }
        #endregion

        #region Constructor
        public RocksViewModel(System.Collections.Generic.List<Models.Rock> rocks)
        {
            instance = this;
            this.rocks = rocks;
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();
            Rocks = new ObservableCollection<Rock>(rocks.OrderBy(c => c.Name));
            CheckData();
        }
        #endregion

        #region Singleton
        static RocksViewModel instance;

        public static RocksViewModel GetInstante()
        {
            if (instance == null)
            {
                return new RocksViewModel(new List<Rock>());
            }

            return instance;
        }
        #endregion


        #region Methods
        void CheckData()
        {
            if (rocks.Count == 0)
            {
                LabelInfo = Languages.Label_Not_Data;
                HasData = true;
            }
            else
            {
                HasData = false;
            }
        }
        public void AddMenu(Rock rock)
        {
            IsRefreshing = true;
            rocks.Add(rock);
            Rocks = new ObservableCollection<Rock>(rocks.OrderBy(c => c.Name));
            CheckData();
            IsRefreshing = false;
        }
        public void UpdateMenu(Rock rock)
        {
            IsRefreshing = true;
            var oldItem = rocks.Where(c => c.RockId == rock.RockId).FirstOrDefault();
            oldItem = rock;
            Rocks = new ObservableCollection<Rock>(rocks.OrderBy(c => c.Name));
            CheckData();
            IsRefreshing = false;
        }
        public async void DeleteCategory(Rock rock)
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
                "/Rocks",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken,
                rock);

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
            rocks.Remove(rock);

            Rocks = new ObservableCollection<Rock>(rocks.OrderBy(c => c.Name));
            CheckData();
            IsRefreshing = false;
        }
        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(SearchRock);
            }
        }

        private void SearchRock()
        {
            IsRefreshing = true;
            if (string.IsNullOrEmpty(Filter))
            {
                Rocks = new ObservableCollection<Rock>(rocks
                    .OrderBy(c => c.Name));
                CheckData();
            }
            else
            {
                Rocks = new ObservableCollection<Rock>(rocks
                .Where(c => c.Descripcion != null && c.Descripcion.ToLower().Contains(Filter.ToLower()) ||
                c.Name != null && c.Name.ToLower().Contains(Filter.ToLower()))
                .OrderBy(c => c.Name));
                if (Rocks.Count == 0)
                {
                    LabelInfo = Languages.Label_Not_Coincidences;
                    HasData = true;
                }
            }
            
            IsRefreshing = false;
        }
        #endregion
    }
}
