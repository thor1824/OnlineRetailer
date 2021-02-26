using System;
using System.Collections.Generic;
using System.Text;

namespace Domane.Model
{
    public class OrderLine
    {
        public int OrderLineId { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Order Order { get; set; }        
        public int OrderId { get; set; }
    }
}
