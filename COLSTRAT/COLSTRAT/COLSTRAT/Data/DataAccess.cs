﻿namespace COLSTRAT.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using COLSTRAT.Interfaces;
    using COLSTRAT.Models;
    using SQLite.Net;
    using SQLiteNetExtensions.Extensions;
    using Xamarin.Forms;

    public class DataAccess : IDisposable
    {//TODO corregir childrens
        SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Platform,
            System.IO.Path.Combine(config.DirectoryDB, "COLSTRAIT.db3"));
            connection.CreateTable<IgneousRock>();
            connection.CreateTable<MetamorphicRock>();
            connection.CreateTable<SedimentaryRock>();
        }

        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        public T First<T>(bool WithChildren) where T : class
        {
            if (WithChildren)
            {
                return connection.Table<T>().FirstOrDefault();
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
                return connection.Table<T>().ToList();
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
                return connection.Table<T>().ToList()
                    .FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection
                    .Table<T>()
                    .FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}