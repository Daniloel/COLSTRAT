namespace COLSTRAT.Backend.Models
{
    using COLSTRAT.Domain;

    public class DataContextLocal : DataContext
    {
        public System.Data.Entity.DbSet<COLSTRAT.Domain.Customer.Customer> Customers { get; set; }
    }
}