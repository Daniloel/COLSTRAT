namespace COLSTRAT.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Models;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using Xamarin.Forms;

    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Platform,
                System.IO.Path.Combine(config.DirectoryDB, "COLSTRAT.db3"));
            connection.CreateTable<MainMenu>();
            connection.CreateTable<Category>();
            connection.CreateTable<RocksMenu>();
            connection.CreateTable<GeneralItem>();
            connection.CreateTable<Rock>();
            connection.CreateTable<TokenResponse>();
            connection.CreateTable<Customer>();
        }

        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }
        public void InsertWithChildrens<T>(T model)
        {
            connection.InsertWithChildren(model);
        }
        public void Update<T>(T model)
        {
            connection.Update(model);
        }
        public void UpdateWithChildren<T>(T model)
        {
            connection.UpdateWithChildren(model);
        }
        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        public List<T> GetList<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }

        public T Find<T>(int pk, bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.GetAllWithChildren<T>()
                                 .FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection.Table<T>()
                                 .FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }

}