using Or.Domain.Model;
using Or.Domain.Model.SharedModels;
using System;
using System.Collections.Generic;

namespace Or.Micro.Orders.Models
{
    public class OrderConverter : IConverter<Order, OrderDto>
    {
        public OrderConverter(IConverter<OrderLine, OrderLineDto> olConverter, IConverter<OrderStatus, OrderStatusDto> osConverter)
        {
            OlConverter = olConverter;
            OsConverter = osConverter;
        }

        public IConverter<OrderLine, OrderLineDto> OlConverter { get; }
        public IConverter<OrderStatus, OrderStatusDto> OsConverter { get; }

        public Order Convert(OrderDto model)
        {
            var temp = new List<OrderLine>();
            foreach (var line in model.OrderLines)
            {
                temp.Add(OlConverter.Convert(line));
            }
            return new Order
            {
                CustomerId = model.CustomerId,
                Date = model.Date,
                OrderId = model.OrderId,
                Status = OsConverter.Convert(model.Status.Value),
                OrderLines = temp

            };
        }

        public OrderDto Convert(Order model)
        {
            var temp = new List<OrderLineDto>();
            foreach (var line in model.OrderLines)
            {
                temp.Add(OlConverter.Convert(line));
            }
            return new OrderDto
            {
                CustomerId = model.CustomerId,
                Date = model.Date,
                OrderId = model.OrderId,
                Status = OsConverter.Convert(model.Status.Value),
                OrderLines = temp
            };
        }
    }
}
