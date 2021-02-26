using Domain.Model.ServiceFacades;
using Domain.Storage;
using Domane.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Micro.OrderDAOService
{
    class Program
    {
        static void Main(string[] args)
        {
            var sp = BuildService();


            var dbContext = sp.GetService<RetailContext>();
            var dbInitializer = sp.GetService<IDbInitializer>();
            dbInitializer.Initialize(dbContext);

            Console.ReadLine();
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            services.AddScoped<IRepository<Order>, OrderRepository>();

            services.AddDbContext<RetailContext>(opt => opt.UseInMemoryDatabase("RetailDB"));
            services.AddTransient<IDbInitializer, DbInitializer>();
            return services.BuildServiceProvider();
        }
    }
}
