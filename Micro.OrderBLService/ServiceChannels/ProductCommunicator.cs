using EasyNetQ;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RetailApi.Micro.OrderBLService.ServiceChannels
{
    public class ProductCommunicator : IProductService
    {
        private IBus _bus;
        public ProductCommunicator(IBus bus)
        {
            _bus = bus;
        }

        public async Task<Product> GetAsync(int productId)
        {
            var request = new GetBLRequest<Product>() { Id = productId };
            var response = await _bus.Rpc.RequestAsync<GetBLRequest<Product>, GetBLResponse<Product>>(request);
            return response.Payload;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var request = new GetBLRequest<IEnumerable<Product>>();
            var response = await _bus.Rpc.RequestAsync<GetBLRequest<IEnumerable<Product>>, GetBLResponse<IEnumerable<Product>>>(request);
            return response.Payload;
        }
    }


}
