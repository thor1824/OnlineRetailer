using Microsoft.EntityFrameworkCore;
using Or.Domain.Model.ServiceFacades;
using Or.Micro.Products.Data;
using Or.Micro.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Micro.Products.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ProductContext _ctx;

        public ProductRepository(ProductContext context)
        {
            _ctx = context;
        }

        public async Task EditAsync(Product entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _ctx.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _ctx.Products.ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var product = await _ctx.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            _ctx.Products.Remove(product);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Product> AddAync(Product entity)
        {
            var newEntry = await _ctx.Products.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return newEntry.Entity;
        }
    }
}
