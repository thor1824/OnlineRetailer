using Or.Micro.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Micro.Orders.Services
{
    public interface IOrderService
    {
        Task<Order> GetAsync(int orderId);

        Task<IEnumerable<Order>> GetAllByCustomerAsync(int customerId);

        Task<Order> AddAsync(Order order);
        Task ChangeStatusAsync(int OrderId, OrderStatus newStatus);
        Task HandleStockResultAsync(int orderId, bool accepted);
        Task HandleCustomerResultAsync(int orderId, bool accepted);
        Task<bool> HasOutStandingBill(int customerId);
        Task HandleOutStandingBillsAsync(int orderId, bool accepted);
    }
}
