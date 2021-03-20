using EasyNetQ;
using Or.Micro.Orders.MessageGateways;
using Or.Micro.Orders.Models;
using Or.Micro.Orders.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Micro.Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;
        private readonly IProductMessageGateway _pServ;
        private readonly ICustomerMessageGateway _cServ;
        private readonly IOrderMessageGateway _oServ;

        public IBus Bus { get; }

        public OrderService(IOrderRepository repo, IProductMessageGateway pServ, ICustomerMessageGateway cServ, IOrderMessageGateway oServ, IBus bus)
        {
            _repo = repo;
            _pServ = pServ;
            _cServ = cServ;
            _oServ = oServ;
            Bus = bus;
        }

        public async Task<Order> GetAsync(int orderId)
        {
            return await _repo.GetAsync(orderId);
        }

        public async Task<IEnumerable<Order>> GetAllByCustomerAsync(int customerId)
        {
            return await _repo.GetByCustomerIdAsync(customerId);
        }

        public async Task<Order> AddAsync(Order order)
        {
            bool customerExists = await _cServ.RpcExistsAsync(order.CustomerId);
            if (!customerExists)
            {
                throw new Exception($"Customer {order.CustomerId} does not exist");
            }

            bool bills = await HasOutStandingBill(order.CustomerId);
            if (bills)
            {
                throw new Exception($"Customer {order.CustomerId} has unpaid bills, order was rejected");
            }

            order.Status = OrderStatus.Submitted;
            var newOrder = await _repo.AddAync(order);

            await _cServ.PublishCustomerValidationRequestAsync(newOrder.OrderId.Value, newOrder.CustomerId);
            //await _oServ.PublishOutstandingBillsRequestAsync(newOrder.OrderId.Value, newOrder.CustomerId);

            Console.WriteLine("Order was submitted");

            return order;
        }

        public async Task ChangeStatusAsync(int orderId, OrderStatus newStatus)
        {
            var order = await GetAsync(orderId);

            order.Status = newStatus;

            await _repo.EditAsync(order);

            await _oServ.PublishStatusChangeAsync(order);
        }



        public async Task<bool> HasOutStandingBill(int customerId)
        {
            var orders = await GetAllByCustomerAsync(customerId);

            foreach (var item in orders)
            {
                if (item.Status == OrderStatus.Unpaid)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task HandleOutStandingBillsAsync(int orderId, bool accepted)
        {
            await HandleCustomerResultAsync(orderId, accepted);
        }

        public async Task HandleCustomerResultAsync(int orderId, bool accepted)
        {
            var order = await GetAsync(orderId);

            if (order.Status == OrderStatus.Rejected)
            {
                return;
            }

            if (!accepted)
            {
                order.Status = OrderStatus.Rejected;
                await _repo.EditAsync(order);

                await _oServ.PublishStatusChangeAsync(order);

                return;
            }

            if (accepted)
            {

                await _pServ.PublishStockValidationRequestAsync(order);
                return;
            }

        }

        public async Task HandleStockResultAsync(int orderId, bool accepted)
        {
            Order order = await GetAsync(orderId);

            if (order.Status == OrderStatus.Rejected)
            {
                return;
            }

            if (!accepted)
            {
                order.Status = OrderStatus.Rejected;
                await _repo.EditAsync(order);

                await _oServ.PublishStatusChangeAsync(order);

                return;
            }

            order.Status = OrderStatus.Completed;
            await _repo.EditAsync(order);
            await _oServ.PublishStatusChangeAsync(order);
        }
    }
}
