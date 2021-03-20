namespace Or.Domain.Model.Messages
{
    public class CustomerValidationMessage
    {
        public int CustomerId { get; set; }
        public int OrderId { get; set; }

    }

    public class CustomerValidationVerdict
    {
        public int OrderId { get; set; }

        public bool Verdict { get; set; }

    }


}
