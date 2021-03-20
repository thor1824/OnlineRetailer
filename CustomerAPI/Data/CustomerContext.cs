using Microsoft.EntityFrameworkCore;
using Or.Micro.Customers.Models;

namespace Or.Micro.Customers.Data
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
