namespace Or.Micro.Products.Data
{
    public interface IDbInitializer
    {
        void Initialize(ProductContext context);
    }
}
