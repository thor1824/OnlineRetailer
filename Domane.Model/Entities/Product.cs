using System;
using System.Collections.Generic;

namespace Domane.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int ItemsInStock { get; set; }
        public int ItemsReserved { get; set; }
        public IList<OrderLine> OrderLines { get; set; }

    }
}
