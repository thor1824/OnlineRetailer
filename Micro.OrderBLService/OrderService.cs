using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.OrderBLService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IProductService _pServ;
        private readonly ICustomerService _cServ;

        public OrderService(IOrderRepository repo, IProductService pServ, ICustomerService cServ)
        {
            _repo = repo;
            _pServ = pServ;
            _cServ = cServ;
        }

        public async Task<Order> AddAsync(Order order)
        {
            // check if UserID exist
            var cust = await _cServ.GetAsync(order.CustomerId);
            if (cust == null) {
                // else throw error
                throw new Exception($"Order Rejected: Customer does not exist");
            }

            // Check if credit standing good
            if (cust.CreditStanding < 0) {
                // else throw error
                throw new Exception("Order Rejected: Credit standing is too low");
            }
            

            // check if cust has unpaid orders
            var result = await _repo.GetByCustomerIdAsync(order.CustomerId);
            var unpaid = result.FirstOrDefault(o => o.Status != OrderStatus.Paid && o.Status != OrderStatus.Cancelled);
            if (unpaid != null) {
                // else throw error
                throw new Exception("Order Rejected: Customer has outstanding bills");
            }

            // get Products

            // Check inventory if enough
            var temp = new List<Product>();
            foreach (var line in order.OrderLines)
            {
                var prod = await _pServ.GetAsync(line.ProductId);
                if(prod ==  null)
                {
                    throw new Exception("Order Rejected: Product Does not exist");
                }
                if (prod.ItemsInStock < line.Quantity)
                {
                    // else throw error
                    throw new Exception("Order Rejected: Product quantity too low");
                }
                temp.Add(prod);
                line.Product = prod;

                line.Product.ItemsInStock = line.Product.ItemsInStock - line.Quantity;
                line.Product.ItemsReserved = line.Product.ItemsReserved + line.Quantity;
            }

            return await _repo.AddAync(order);
        }

        public async Task ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            var update = new Order { OrderId = orderId, Status = newStatus };
            if (newStatus == OrderStatus.Shipped) {
                var order = await _repo.GetAsync(orderId);
                foreach (var line in order.OrderLines)
                {
                    line.Product.ItemsReserved = line.Product.ItemsReserved - line.Quantity;
                }
                order.Status = newStatus;
                update = order;
            }
            await _repo.EditAsync(update);
        }

        public async Task<Order> GetAsync(int orderId)
        {
            return await _repo.GetAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllByCustomerAsync(int customerId)
        {
            return await _repo.GetByCustomerIdAsync(customerId);
        }
    }
}
