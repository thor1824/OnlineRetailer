using Microsoft.EntityFrameworkCore;
using Or.Micro.Products.Models;

namespace Or.Micro.Products.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
