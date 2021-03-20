using EasyNetQ;
using Or.Domain.Model.Messages;
using System.Threading.Tasks;

namespace Or.Micro.Orders.MessageGateways.Impl
{
    public class CustomerMessageGateway : ICustomerMessageGateway
    {
        private IBus _bus;
        public CustomerMessageGateway(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishCustomerValidationRequestAsync(int orderId, int customerId)
        {
            var message = new CustomerValidationMessage { OrderId = orderId, CustomerId = customerId };
            await _bus.PubSub.PublishAsync(message).ConfigureAwait(false);
        }

        public async Task<bool> RpcExistsAsync(int customerId)
        {
            var request = new CustomerExistsRequest { CustomerId = customerId };
            var response = await _bus.Rpc.RequestAsync<CustomerExistsRequest, CustomerExistsResponse>(request);
            return response.Verdict;
        }
    }

}
