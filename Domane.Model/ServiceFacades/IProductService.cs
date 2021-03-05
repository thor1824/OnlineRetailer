using Or.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Or.Domain.Model.ServiceFacades
{
    public interface IProductService
    {
        Task<Product> GetAsync(int productId);

        Task<IEnumerable<Product>> GetAllAsync();
    }
}
