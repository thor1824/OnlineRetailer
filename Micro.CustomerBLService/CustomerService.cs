using Domane.Model;
using Domane.Model.ServiceFacades;
using EasyNetQ;
using RetailApi.Domain.Model.ServiceFacades;
using System;
using System.Threading.Tasks;

namespace Micro.CustomerBLService
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repo;

        public CustomerService(IRepository<Customer> repo)
        {
            _repo = repo;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            return await _repo.AddAync(customer);
        }

        public async Task DeleteAsync(int customerId)
        {
            await _repo.RemoveAsync(customerId);
        }

        public async Task<Customer> GetAsync(int customerId)
        {
            return await _repo.GetAsync(customerId);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _repo.EditAsync(customer);
        }
    }
}
