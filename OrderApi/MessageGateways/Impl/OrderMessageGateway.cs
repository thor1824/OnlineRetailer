

using EasyNetQ;
using Or.Domain.Model.Messages;
using Or.Micro.Orders.Models;
using System;
using System.Threading.Tasks;

namespace Or.Micro.Orders.MessageGateways.Impl
{
    public class OrderMessageGateway : IOrderMessageGateway
    {
        private IBus _bus;
        public OrderMessageGateway(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishOutstandingBillsRequestAsync(int orderId, int customerId)
        {
            var message = new OutstandingBillsRequest { CustomerId = customerId, OrderId = orderId };
            await _bus.PubSub.PublishAsync(message).ConfigureAwait(false);
        }

        public async Task PublishStatusChangeAsync(Order order, string optionalMessage = "")
        {
            var topic = GetStatusTopic(order.Status.Value);

            var converter = new OrderConverter(new OrderLineConverter(), new OrderStatusConverter());
            var message = new StatusChangeMessage { Order = converter.Convert(order), OptionalMessage = optionalMessage };
            await _bus.PubSub.PublishAsync(message, topic).ConfigureAwait(false);
        }

        private string GetStatusTopic(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Submitted:
                    return "submitted";

                case OrderStatus.ValidatedCustomer:
                    return "validatedCustomer";

                case OrderStatus.ValidatedStock:
                    return "validatedStock";

                case OrderStatus.Completed:
                    return "completed";

                case OrderStatus.Rejected:
                    return "rejected";

                case OrderStatus.Cancelled:
                    return "cancelled";

                case OrderStatus.Shipped:
                    return "shipped";

                case OrderStatus.Paid:
                    return "paid";

                case OrderStatus.Unpaid:
                    return "unpaid";

                default:
                    throw new InvalidOperationException();
            }
        }
    }

    
}
