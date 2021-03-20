using Microsoft.EntityFrameworkCore;
using Or.Micro.Orders.Models;

namespace Or.Micro.Orders.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderLine>()
                .HasKey(ol => ol.OrderLineId);

            modelBuilder.Entity<Order>()
                .HasMany(bc => bc.OrderLines);
        }
    }
}
