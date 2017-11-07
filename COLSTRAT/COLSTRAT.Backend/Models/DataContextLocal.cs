namespace COLSTRAT.Backend.Models
{
    using COLSTRAT.Domain;
    using System.Data.Entity;

    public class DataContextLocal : DataContext
    {
        public System.Data.Entity.DbSet<Category> Categories { get; set; }
    }
}