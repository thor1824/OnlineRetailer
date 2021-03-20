using Or.Domain.Model.ServiceFacades;
using Or.Micro.Customers.Models;
using System.Threading.Tasks;

namespace Or.Micro.Customers.Service
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
