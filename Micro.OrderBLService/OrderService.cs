using Domane.Model;
using Domane.Model.ServiceFacades;
using System;
using System.Collections.Generic;

namespace Micro.OrderBLService
{
    public class OrderService : IOrderService
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
