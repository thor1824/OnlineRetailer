using Or.Domain.Model;
using Or.Domain.Model.SharedModels;
using System;

namespace Or.Micro.Orders.Models
{
    public class OrderLineConverter : IConverter<OrderLine, OrderLineDto>
    {
        public OrderLine Convert(OrderLineDto model)
        {
            return new OrderLine
            {
                OrderLineId = model.OrderLineId,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };
        }

        public OrderLineDto Convert(OrderLine model)
        {
            return new OrderLineDto
            {
                OrderLineId = model.OrderLineId,
                ProductId = model.ProductId,
                Quantity = model.Quantity
            };
        }
    }
}
