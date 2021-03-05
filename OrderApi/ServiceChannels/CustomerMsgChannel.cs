using EasyNetQ;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Or.Micro.Orders.ServiceChannels
{
    public class CustomerMsgChannel : ICustomerService
    {
        private readonly IBus _bus;

        public CustomerMsgChannel(IBus bus)
        {
            _bus = bus;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            var request = new CreateBLRequest<Customer>() { Payload = customer };
            var response = await _bus.Rpc.RequestAsync<CreateBLRequest<Customer>, CreateBLResponse<Customer>>(request);
            return response.Payload;
        }

        public async Task DeleteAsync(int customerId)
        {
            var request = new DeleteBLRequest<Customer>() { Id = customerId };
            await _bus.Rpc.RequestAsync<DeleteBLRequest<Customer>, DeleteBLRequest<Customer>>(request);
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            var request = new GetBLRequest<Customer>() { Id = customerId };
            var response = await _bus.Rpc.RequestAsync<GetBLRequest<Customer>, GetBLResponse<Customer>>(request);
            return response.Payload;
        }

        public async Task UpdateAsync(Customer customer)
        {
            var request = new UpdateBLRequest<Customer>() { Payload = customer };
            await _bus.Rpc.RequestAsync<UpdateBLRequest<Customer>, UpdateBLResponse<Customer>>(request);
        }
    }
}
