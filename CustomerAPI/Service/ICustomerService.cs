using Or.Micro.Customers.Models;
using System.Threading.Tasks;

namespace Or.Micro.Customers.Service
{
    public interface ICustomerService
    {
        Task<Customer> GetAsync(int customerId);

        Task<Customer> AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task DeleteAsync(int customerId);
    }
}
