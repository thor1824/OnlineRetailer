using Or.Domain.Model.SharedModels;
using System.Collections.Generic;

namespace Or.Domain.Model.Messages
{
    public class StockValidationRequest
    {
        public int OrderId { get; set; }
        public IEnumerable<OrderLineDto> Orderlines { get; set; }
    }

    public class StockValidationResponse
    {
        public int OrderId { get; set; }

        public bool Verdict { get; set; }
    }
}
