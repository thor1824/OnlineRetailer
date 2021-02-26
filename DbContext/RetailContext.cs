using Domane.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain.Storage
{
    public class RetailContext : DbContext
    {
        public RetailContext(DbContextOptions<RetailContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
