namespace Or.Micro.Orders.Data
{
    public interface IDbInitializer
    {
        void Initialize(OrderContext context);
    }
}
