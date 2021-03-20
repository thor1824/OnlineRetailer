using Or.Domain.Model.ServiceFacades;
using Or.Domain.Model.SharedModels;
using Or.Micro.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Or.Micro.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repo;

        public ProductService(IRepository<Product> repo)
        {
            _repo = repo;
        }

        public async Task<Product> GetAsync(int productId)
        {
            return await _repo.GetAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task UpdateAsync(Product prod)
        {
            await _repo.EditAsync(prod);
        }
    }
}
