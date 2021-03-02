using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domane.Model.ServiceFacades
{
    public interface IProductService
    {
        Task<Product> GetAsync(int productId);

        Task<IEnumerable<Product>> GetAllAsync();
    }
}
