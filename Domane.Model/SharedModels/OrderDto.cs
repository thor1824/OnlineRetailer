using System;
using System.Collections.Generic;

namespace Or.Domain.Model.SharedModels
{
    public class OrderDto
    {
        public int? OrderId { get; set; }
        public DateTime? Date { get; set; }
        public OrderStatusDto? Status { get; set; }
        public IList<OrderLineDto> OrderLines { get; set; }
        public int CustomerId { get; set; }
    }
}
