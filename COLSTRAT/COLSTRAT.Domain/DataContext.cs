namespace COLSTRAT.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rock> Rocks { get; set; }
        public DbSet<MohsScale> MohsScales { get; set; }
        public DbSet<TypeOfRock> TypeOfRocks { get; set; }
        public DataContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}