namespace Domain.Storage
{
    public interface IDbInitializer
    {
        void Initialize(RetailContext context);
    }
}
