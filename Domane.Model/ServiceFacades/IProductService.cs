using System;
using System.Collections.Generic;
using System.Text;

namespace Domane.Model.ServiceFacades
{
    public interface IProductService
    {
        Product Get(int productId);

        IList<Product> GetAll();
    }
}
