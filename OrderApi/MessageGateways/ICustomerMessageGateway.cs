using System.Threading.Tasks;

namespace Or.Micro.Orders.MessageGateways
{
    public interface ICustomerMessageGateway
    {
        Task<bool> RpcExistsAsync(int customerId);
        Task PublishCustomerValidationRequestAsync(int value, int customerId);
    }
}