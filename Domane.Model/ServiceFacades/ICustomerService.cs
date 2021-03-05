using Or.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Or.Domain.Model.ServiceFacades
{
    public interface ICustomerService
    {
        Task<Customer> GetAsync(int customerId);

        Task<Customer> AddAsync(Customer customer);

        Task UpdateAsync(Customer customer);

        Task DeleteAsync(int customerId);
    }
}
