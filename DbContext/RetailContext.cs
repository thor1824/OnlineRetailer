using Microsoft.EntityFrameworkCore;
using Or.Domain.Model.Entities;

namespace Or.Domain.Storage
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
            modelBuilder.Entity<OrderLine>()
                .HasKey(ol => ol.OrderLineId);

            modelBuilder.Entity<OrderLine>()
                .HasOne(bc => bc.Order)
                .WithMany(b => b.OrderLines)
                .HasForeignKey(bc => bc.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderLine>()
                .HasOne(bc => bc.Product)
                .WithMany(c => c.OrderLines)
                .HasForeignKey(bc => bc.ProductId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
