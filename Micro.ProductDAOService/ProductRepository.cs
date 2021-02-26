using Domain.Model.ServiceFacades;
using Domain.Storage;
using Domane.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Micro.ProductDAOService
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly RetailContext db;

        public ProductRepository(RetailContext context)
        {
            db = context;
        }

        Product IRepository<Product>.Add(Product entity)
        {
            var newProduct = db.Products.Add(entity).Entity;
            db.SaveChanges();
            return newProduct;
        }

        void IRepository<Product>.Edit(Product entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        Product IRepository<Product>.Get(int id)
        {
            return db.Products.FirstOrDefault(p => p.ProductId == id);
        }

        IEnumerable<Product> IRepository<Product>.GetAll()
        {
            return db.Products.ToList();
        }

        void IRepository<Product>.Remove(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.ProductId == id);
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }
}
