using Or.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailApi.Domain.Model.Messages.Specialised
{

    public class ByCustomerBLRequest
    {
        public int CustomerId { get; init; }
    }
    public class ByCustomerBLResponse
    {
        public IEnumerable<Order> Payload { get; set; }
    }

    public class ByCustomerDaoRequest
    {
        public int CustomerId { get; init; }
    }
    public class ByCustomerDaoResponse
    {
        public IEnumerable<Order> Payload { get; set; }
    }
}
