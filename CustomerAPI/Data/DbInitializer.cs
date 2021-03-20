namespace Or.Micro.Customers.Data
{
    public class DbInitializer : IDbInitializer
    {
        // This method will create and seed the database.
        public void Initialize(CustomerContext context)
        {
            if (context.Database.EnsureCreated())
            {
            };

            //Seed data here
            context.SaveChanges();
        }
    }
}
