using System;
using System.Collections.Generic;
using System.Text;

namespace Domane.Model.ServiceFacades
{
    public interface IOrderService
    {
        Order Get(int orderId);

        IList<Order> GetAllByCustomer(int customerId);

        Order Add(Order order);
        void ChangeStatus(int OrderId, OrderStatus newStatus);
    }
}
