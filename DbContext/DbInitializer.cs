using Domane.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Storage
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(RetailContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            //// Look for any Products
            //if (context.Orders.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //List<Order> orders = new List<Order>
            //{
            //    new Order { Date = DateTime.Today, ProductId = 1, Quantity = 2 }
            //};

            //context.Orders.AddRange(orders);
            //context.SaveChanges();

            //// Look for any Products
            //if (context.Products.Any())
            //{
            //    return;   // DB has been seeded
            //}

            //List<Product> products = new List<Product>
            //{
            //    new Product { Name = "Hammer", Price = 100, ItemsInStock = 10, ItemsReserved = 0 },
            //    new Product { Name = "Screwdriver", Price = 70, ItemsInStock = 20, ItemsReserved = 0 },
            //    new Product { Name = "Drill", Price = 500, ItemsInStock = 2, ItemsReserved = 0 }
            //};

            //context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
