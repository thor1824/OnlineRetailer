using Domane.Model;
using Domane.Model.ServiceFacades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Requester
{
    public class OrderCommunicator : IOrderService
    {
        public Order Add(Order order)
        {
            throw new NotImplementedException();
        }

        public void ChangeStatus(int OrderId, OrderStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Order Get(int orderId)
        {
            throw new NotImplementedException();
        }

        public IList<Order> GetAllByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
