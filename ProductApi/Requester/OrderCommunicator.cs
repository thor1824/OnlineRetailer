using Domane.Model;
using Domane.Model.ServiceFacades;
using System;
using System.Collections.Generic;

namespace ProductApi.Requester
{
    public class ProductCommunicator : IProductService
    {
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
