﻿using System;
using System.Collections.Generic;

namespace Or.Domain.Model.SharedModels
{
    public class CustomerDto
    {
        public int? CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public decimal? CreditStanding { get; set; }
    }
}
