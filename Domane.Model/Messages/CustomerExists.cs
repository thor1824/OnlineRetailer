namespace Or.Domain.Model.Messages
{
    public class CustomerExistsRequest
    {
        public int CustomerId { get; set; }
    }
    public class CustomerExistsResponse
    {
        public bool Verdict { get; set; }

    }
}
