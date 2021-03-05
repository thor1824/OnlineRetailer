using EasyNetQ;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro.ProductBLService
{
    public class ProductRepoCom : IRepository<Product>
    {
        private readonly IBus _bus;

        public ProductRepoCom(IBus bus) {
            _bus = bus;
        }
        public Task<Product> AddAync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetAsync(int id)
        {
            var request = new GetDaoRequest<Product>() { Id = id};
            var response = await _bus.Rpc.RequestAsync<GetDaoRequest<Product>, GetDaoResponse<Product>>(request);
            return response.Payload;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var request = new GetDaoRequest<IEnumerable<Product>>();
            var response = await _bus.Rpc.RequestAsync<GetDaoRequest<IEnumerable<Product>>, GetDaoResponse<IEnumerable<Product>>>(request);
            return response.Payload;
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
