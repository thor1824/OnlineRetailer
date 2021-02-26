using Domain.Model.ServiceFacades;
using Domane.Model;
using Domane.Model.ServiceFacades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Micro.ProductBLService
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repo;

        public ProductService(IRepository<Product> repo)
        {
            _repo = repo;
        }

        public Product Get(int productId)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
