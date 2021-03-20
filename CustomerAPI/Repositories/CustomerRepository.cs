using Microsoft.EntityFrameworkCore;
using Or.Domain.Model.ServiceFacades;
using Or.Micro.Customers.Data;
using Or.Micro.Customers.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Micro.Customers.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly CustomerContext _ctx;

        public CustomerRepository(CustomerContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Customer> AddAync(Customer entity)
        {
            var newEntry = await _ctx.Customers.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return newEntry.Entity;
        }

        public async Task EditAsync(Customer entity)
        {
            if (entity == null)
            {
                return;
            }
            var customer = await _ctx.Customers.FirstOrDefaultAsync(x => x.CustomerId == entity.CustomerId);
            if (entity.Name != null)
            {
                customer.Name = entity.Name;
            }

            if (entity.Email != null)
            {
                customer.Email = entity.Email;
            }

            if (entity.Phone != null)
            {
                customer.Phone = entity.Phone;
            }

            if (entity.BillingAddress != null)
            {
                customer.BillingAddress = entity.BillingAddress;
            }

            if (entity.ShippingAddress != null)
            {
                customer.ShippingAddress = entity.ShippingAddress;
            }

            if (entity.CreditStanding != null)
            {
                customer.CreditStanding = entity.CreditStanding;
            }
            await _ctx.SaveChangesAsync();
        }

        public async Task<Customer> GetAsync(int id)
        {
            return await _ctx.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustomerId == id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _ctx.Customers.AsNoTracking().ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var customer = await _ctx.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
            _ctx.Customers.Remove(customer);
            await _ctx.SaveChangesAsync();
        }
    }
}
