using System;
using System.Collections.Generic;
using System.Text;

namespace Or.Domain.Model.SharedModels
{
    public class OrderLineDto
    {
        public int? OrderLineId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }
    }
}
