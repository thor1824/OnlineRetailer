

using EasyNetQ;
using Or.Domain.Model.Messages;
using Or.Domain.Model.SharedModels;
using Or.Micro.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Or.Micro.Orders.MessageGateways.Impl
{
    public class ProducMessageGateway : IProductMessageGateway
    {
        private IBus _bus;
        public ProducMessageGateway(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishStockValidationRequestAsync(Order order)
        {
            var temp = new List<OrderLineDto>();
            var converter = new OrderLineConverter();
            foreach (var line in order.OrderLines)
            {
                temp.Add(converter.Convert(line));
            }
            var message = new StockValidationRequest { OrderId = order.OrderId.Value, Orderlines = temp };
            await _bus.PubSub.PublishAsync(message).ConfigureAwait(false);
        }
    }
}
