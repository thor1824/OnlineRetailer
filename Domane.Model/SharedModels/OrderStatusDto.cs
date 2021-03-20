using System;
using System.Collections.Generic;
using System.Text;

namespace Or.Domain.Model.SharedModels
{
    public enum OrderStatusDto
    {
        Submitted, ValidatedCustomer, ValidatedStock, Completed, Rejected, Cancelled, Shipped, Paid, Unpaid
    }
}
