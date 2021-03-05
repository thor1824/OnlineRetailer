using EasyNetQ;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.CustomerBLService
{
    public class CustomerRepoCom : IRepository<Customer>
    {
        private readonly IBus _bus;

        public CustomerRepoCom(IBus bus) {
            _bus = bus;
        }
        public async Task<Customer> AddAync(Customer entity)
        {
            var request = new CreateDaoRequest<Customer>() { Payload = entity };
            var response = await _bus.Rpc.RequestAsync<CreateDaoRequest<Customer>, CreateDaoResponse<Customer>>(request);
            return response.Payload;
        }

        public async Task EditAsync(Customer entity)
        {
            var request = new UpdateDaoRequest<Customer>() { Payload = entity };
            await _bus.Rpc.RequestAsync<UpdateDaoRequest<Customer>, UpdateDaoResponse<Customer>>(request);
        }

        public async Task<Customer> GetAsync(int id)
        {
            var request = new GetDaoRequest<Customer>() { Id = id };
            var response = await _bus.Rpc.RequestAsync<GetDaoRequest<Customer>, GetDaoResponse<Customer>>(request);
            return response.Payload;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var request = new GetDaoRequest<IEnumerable<Customer>>();
            var response = await _bus.Rpc.RequestAsync<GetDaoRequest<IEnumerable<Customer>>, GetDaoResponse<IEnumerable<Customer>>>(request);
            return response.Payload;
        }

        public async Task RemoveAsync(int id)
        {
            var request = new DeleteDaoRequest<Customer>() { Id = id };
            await _bus.Rpc.RequestAsync<DeleteDaoRequest<Customer>, DeleteDaoRequest<Customer>>(request);
        }
    }
}
