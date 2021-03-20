using Or.Domain.Model.SharedModels;
using Or.Micro.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Or.Micro.Products.Services
{
    public interface IProductService
    {
        Task<Product> GetAsync(int productId);

        Task<IEnumerable<Product>> GetAllAsync();
        Task UpdateAsync(Product prod);
    }
}
