namespace COLSTRAT.Domain
{
    using System.Data.Entity;

    public partial class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }
        
    }
}