using Microsoft.EntityFrameworkCore;
using Or.Micro.Orders.Data;
using Or.Micro.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Or.Micro.Orders.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _ctx;

        public OrderRepository(OrderContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Order> AddAync(Order entity)
        {
            if (entity.Date == null)
                entity.Date = DateTime.Now;

            var entry = await _ctx.Orders.AddAsync(entity);
            await _ctx.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task EditAsync(Order entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
        }

        public async Task<Order> GetAsync(int id)
        {
            return await _ctx.Orders
                .Include(o => o.OrderLines)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _ctx.Orders
                .Include(o => o.OrderLines)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
        {
            return await _ctx.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderLines)
                .ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var order = await _ctx.Orders.FirstOrDefaultAsync(p => p.OrderId == id);
            _ctx.Orders.Remove(order);
            await _ctx.SaveChangesAsync();
        }
    }
}
