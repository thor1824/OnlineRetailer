using Domain.Model.ServiceFacades;
using Domane.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.OrderBLService
{
    public class OrderRepoCom : IRepository<Order>
    {
        public Order Add(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(Order entity)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
