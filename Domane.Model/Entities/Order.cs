using System;
using System.Collections.Generic;

namespace Domane.Model
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime? Date { get; set; }
        public OrderStatus Status { get; set; }

        public IList<OrderLine> OrderLines{get; set;}
    }
}
