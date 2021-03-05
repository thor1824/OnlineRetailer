
using Newtonsoft.Json;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Or.Micro.Orders.ServiceChannels
{
    public class ProductHttpChannel : IProductService
    {
        private string baseUrl = "http://or.micro.products:80/Products/";
        private readonly IHttpClientFactory _clientFactory;

        public ProductHttpChannel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Product> GetAsync(int productId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + productId);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                
                string result = await response.Content.ReadAsStringAsync();
                Product product = JsonConvert.DeserializeObject<Product>(result);
                return product;
            }
            else
            {
                throw new Exception("Http Error: " + response.StatusCode);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }


}
