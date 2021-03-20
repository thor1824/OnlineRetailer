namespace Or.Micro.Orders.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(OrderContext context)
        {
            if (context.Database.EnsureCreated())
            {
            };

            //Seed data here
            context.SaveChanges();
        }
    }
}
