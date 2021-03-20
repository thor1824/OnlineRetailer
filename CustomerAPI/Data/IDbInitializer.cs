namespace Or.Micro.Customers.Data
{
    public interface IDbInitializer
    {
        void Initialize(CustomerContext context);
    }
}
