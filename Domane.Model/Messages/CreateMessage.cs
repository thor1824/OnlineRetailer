namespace RetailApi.Domain.Model.Messages
{
    public class CreateBLRequest<T>
    {
        public T Payload { get; set; }
    }
    public class CreateBLResponse<T>
    {
        public T Payload { get; set; }
    }

    public class CreateDaoRequest<T>
    {
        public T Payload { get; set; }
    }
    public class CreateDaoResponse<T>
    {
        public T Payload { get; set; }
    }
}
