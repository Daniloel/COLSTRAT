namespace COLSTRAT.Backend.Models
{
    using COLSTRAT.Domain;
    using System.Data.Entity;

    public class DataContextLocal : DataContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Rock> Rocks { get; set; }
        public DbSet<MohsScale> MohsScales { get; set; }
    }
}