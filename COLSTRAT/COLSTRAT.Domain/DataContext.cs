namespace COLSTRAT.Domain
{
    using COLSTRAT.Domain.Menu.Categories;
    using COLSTRAT.Domain.Menu.Entity.Fluids;
    using COLSTRAT.Domain.Menu.Entity.Fluids.Valvules;
    using COLSTRAT.Domain.Menu.Entity.Geology;
    using COLSTRAT.Domain.Menu.Entity.Geology.Rocks;
    using COLSTRAT.Domain.Menu.Main;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class DataContext : DbContext
    {
        public DbSet<MainMenu> MainMenu { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FluidsCategory> FluidsCategories { get; set; }
        public DbSet<ValvulesMenu> ValvulesMenu { get; set; }
        public DbSet<Valvule> Valvule { get; set; }
        public DbSet<GeologyCategory> GeologyCategories { get; set; }
        public DbSet<RocksMenu> RocksMenu { get; set; }
        public DbSet<Rock> Rocks { get; set; }
        public DbSet<MohsScale> MohsScales { get; set; }


        public DataContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}