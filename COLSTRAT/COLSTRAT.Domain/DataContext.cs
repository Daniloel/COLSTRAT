namespace COLSTRAT.Domain
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }
        
    }
}