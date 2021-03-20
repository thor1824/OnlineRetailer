
using System;
using System.Collections.Generic;

namespace Or.Micro.Orders.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
        public DateTime? Date { get; set; }
        public OrderStatus? Status { get; set; }
        public IList<OrderLine> OrderLines { get; set; }
        public int CustomerId { get; set; }
    }
}
