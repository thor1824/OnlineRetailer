using System;
using System.Collections.Generic;

namespace Or.Domain.Model.Entities
{
    public class Customer
    {
        public int? CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public decimal? CreditStanding { get; set; }
        public IList<Order> Orders { get; set; }
    }
}
