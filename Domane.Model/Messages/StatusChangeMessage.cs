using Or.Domain.Model.SharedModels;

namespace Or.Domain.Model.Messages
{
    public class StatusChangeMessage
    {
        public OrderDto Order { get; set; }
        public string OptionalMessage { get; set; }
    }
}
