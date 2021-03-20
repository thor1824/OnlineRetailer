using Or.Domain.Model.ServiceFacades;
using Or.Micro.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Micro.Orders.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
    }
}
