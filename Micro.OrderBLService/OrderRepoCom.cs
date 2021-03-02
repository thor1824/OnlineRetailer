using Domane.Model;
using EasyNetQ;
using RetailApi.Domain.Model.Messages;
using RetailApi.Domain.Model.Messages.Specialised;
using RetailApi.Domain.Model.ServiceFacades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Micro.OrderBLService
{
    public class OrderRepoCom : IOrderRepository
    {
        private readonly IBus _bus;

        public OrderRepoCom(IBus bus)
        {
            _bus = bus;
        }

        public async Task<Order> AddAync(Order entity)
        {
            var request = new CreateDaoRequest<Order>() { Payload = entity };
            var response = await _bus.Rpc.RequestAsync<CreateDaoRequest<Order>, CreateDaoResponse<Order>>(request);
            return response.Payload;
        }

        public async Task EditAsync(Order entity)
        {
            var request = new UpdateDaoRequest<Order>() { Payload = entity };
            await _bus.Rpc.RequestAsync<UpdateDaoRequest<Order>, UpdateDaoResponse<Order>>(request);
        }

        public async Task<Order> GetAsync(int id)
        {
            var request = new GetDaoRequest<Order>() { Id = id };
            var response = await _bus.Rpc.RequestAsync<GetDaoRequest<Order>, GetDaoResponse<Order>>(request);
            return response.Payload;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var request = new GetDaoRequest<IEnumerable<Order>>();
            var response = await _bus.Rpc.RequestAsync<GetDaoRequest<IEnumerable<Order>>, GetDaoResponse<IEnumerable<Order>>>(request);
            return response.Payload;
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            var request = new ByCustomerDaoRequest() {CustomerId= customerId };
            var response = await _bus.Rpc.RequestAsync<ByCustomerDaoRequest, ByCustomerDaoResponse>(request);
            return response.Payload;
        }

        public async Task RemoveAsync(int id)
        {
            var request = new DeleteDaoRequest<Order>() { Id = id };
            await _bus.Rpc.RequestAsync<DeleteDaoRequest<Order>, DeleteDaoRequest<Order>>(request);
        }
    }
}
