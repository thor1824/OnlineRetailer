using Domain.Model.ServiceFacades;
using Domane.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.ProductBLService
{
    public class ProductRepoCom : IRepository<Product>
    {
        public Product Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
