
using Newtonsoft.Json;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Or.Micro.Orders.ServiceChannels
{
    public class CustomerHttpChannel : ICustomerService
    {
        private string baseUrl = "http://or.micro.customers:80/Customers/";
        private readonly IHttpClientFactory _clientFactory;

        public CustomerHttpChannel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + customerId);

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                Customer customer = JsonConvert.DeserializeObject<Customer>(result);
                return customer;
            }
            else
            {
                throw new Exception("Http Error: " + response.StatusCode);
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
