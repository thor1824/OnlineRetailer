using System;
using System.Collections.Generic;
using System.Text;

namespace Domane.Model.ServiceFacades
{
    public interface ICustomerService
    {
        Customer Get(int customerId);

        Customer Add(Customer customer);

        void Update(Customer customer);

        void Delete(int customerId);
    }
}
