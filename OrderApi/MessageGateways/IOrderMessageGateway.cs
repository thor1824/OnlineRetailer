using Or.Domain.Model.SharedModels;
using Or.Micro.Orders.Models;
using System.Threading.Tasks;

namespace Or.Micro.Orders.MessageGateways
{
    public interface IOrderMessageGateway
    {
        Task PublishOutstandingBillsRequestAsync(int orderId, int customerId);
        Task PublishStatusChangeAsync(Order order, string optionalMessage = null);
    }
}