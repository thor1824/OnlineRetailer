using EasyNetQ;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using RetailApi.Domain.Model.Messages.Specialised;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Micro.Orders.Requester
{
    public class OrderCommunicator : IOrderService
    {
        private readonly IBus _bus;

        public OrderCommunicator(IBus bus)
        {
            _bus = bus;
        }

        public async Task<Order> AddAsync(Order order)
        {
            var request = new CreateBLRequest<Order>() { Payload = order };
            var response = await _bus.Rpc.RequestAsync<CreateBLRequest<Order>, CreateBLResponse<Order>>(request);
            return response.Payload;
        }

        public async Task ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            var request = new UpdateBLRequest<Order>() { Payload = new Order { OrderId = orderId, Status = newStatus } };
            await _bus.Rpc.RequestAsync<UpdateBLRequest<Order>, UpdateBLResponse<Order>>(request);
        }

        public async Task<Order> GetAsync(int orderId)
        {
            var request = new GetBLRequest<Order>() { Id = orderId };
            var response = await _bus.Rpc.RequestAsync<GetBLRequest<Order>, GetBLResponse<Order>>(request);
            return response.Payload;
        }

        public async Task<IEnumerable<Order>> GetAllByCustomerAsync(int customerId)
        {
            var request = new ByCustomerBLRequest() { CustomerId = customerId };
            var response = await _bus.Rpc.RequestAsync<ByCustomerBLRequest, ByCustomerBLResponse>(request);
            return response.Payload;
        }
    }
}
