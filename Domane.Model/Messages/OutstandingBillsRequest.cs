namespace Or.Domain.Model.Messages
{
    public class OutstandingBillsRequest
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    }
}
