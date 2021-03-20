using Or.Micro.Products.Models;
using System.Collections.Generic;

namespace Or.Micro.Products.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(ProductContext context)
        {
            if (context.Database.EnsureCreated())
            {
            };

            List<Product> products = new List<Product>
            {
                new Product { Name = "Hammer", Price = 100, ItemsInStock = 10, ItemsReserved = 0 },
                new Product { Name = "Screwdriver", Price = 70, ItemsInStock = 20, ItemsReserved = 0 },
                new Product { Name = "Drill", Price = 500, ItemsInStock = 2, ItemsReserved = 0 }
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
