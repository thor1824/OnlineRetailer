using Domane.Model;
using Domane.Model.ServiceFacades;
using RetailApi.Domain.Model.ServiceFacades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Micro.ProductBLService
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
    }
}
