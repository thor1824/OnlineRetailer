using Domain.Storage;
using Domane.Model;
using Microsoft.EntityFrameworkCore;
using RetailApi.Domain.Model.ServiceFacades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.ProductDAOService
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly RetailContext _ctx;

        public ProductRepository(RetailContext context)
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
            return await _ctx.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _ctx.Products.AsNoTracking().ToListAsync();
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
