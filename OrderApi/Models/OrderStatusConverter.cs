using Or.Domain.Model;
using Or.Domain.Model.SharedModels;
using System;

namespace Or.Micro.Orders.Models
{
    public class OrderStatusConverter : IConverter<OrderStatus, OrderStatusDto>
    {
        public OrderStatus Convert(OrderStatusDto model)
        {
            switch (model)
            {
                case OrderStatusDto.Submitted:
                    return OrderStatus.Submitted;

                case OrderStatusDto.ValidatedCustomer:
                    return OrderStatus.ValidatedCustomer;

                case OrderStatusDto.ValidatedStock:
                    return OrderStatus.ValidatedStock;

                case OrderStatusDto.Completed:
                    return OrderStatus.Completed;

                case OrderStatusDto.Rejected:
                    return OrderStatus.Rejected;

                case OrderStatusDto.Cancelled:
                    return OrderStatus.Cancelled;

                case OrderStatusDto.Shipped:
                    return OrderStatus.Shipped;

                case OrderStatusDto.Paid:
                    return OrderStatus.Paid;

                case OrderStatusDto.Unpaid:
                    return OrderStatus.Unpaid;

                default:
                    throw new Exception("Unsupported OrderStatusDto");
            }
        }

        public OrderStatusDto Convert(OrderStatus model)
        {
            switch (model)
            {
                case OrderStatus.Submitted:
                    return OrderStatusDto.Submitted;

                case OrderStatus.ValidatedCustomer:
                    return OrderStatusDto.ValidatedCustomer;

                case OrderStatus.ValidatedStock:
                    return OrderStatusDto.ValidatedStock;

                case OrderStatus.Completed:
                    return OrderStatusDto.Completed;

                case OrderStatus.Rejected:
                    return OrderStatusDto.Rejected;

                case OrderStatus.Cancelled:
                    return OrderStatusDto.Cancelled;

                case OrderStatus.Shipped:
                    return OrderStatusDto.Shipped;

                case OrderStatus.Paid:
                    return OrderStatusDto.Paid;

                case OrderStatus.Unpaid:
                    return OrderStatusDto.Unpaid;

                default:
                    throw new Exception("Unsupported OrderStatus");
            }
        }
    }
}
