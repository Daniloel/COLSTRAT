namespace COLSTRAT.Backend.Models
{
    using COLSTRAT.Domain;
    public class DataContextLocal : DataContext
    {
        public System.Data.Entity.DbSet<Category> Categories { get; set; }
    }
}