using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Or.Domain.Model.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int ItemsInStock { get; set; }
        public int ItemsReserved { get; set; }



        /*[JsonIgnore]*/
        public IEnumerable<OrderLine> OrderLines { get; set; }

    }
}
