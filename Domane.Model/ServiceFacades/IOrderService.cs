using Or.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Domain.Model.ServiceFacades
{
    public interface IOrderService
    {
        Task<Order> GetAsync(int orderId);

        Task<IEnumerable<Order>> GetAllByCustomerAsync(int customerId);

        Task<Order> AddAsync(Order order);
        Task ChangeStatusAsync(int OrderId, OrderStatus newStatus);
    }
}
