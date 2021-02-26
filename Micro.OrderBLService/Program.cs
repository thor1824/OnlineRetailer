using Domain.Model.ServiceFacades;
using Domane.Model;
using Domane.Model.ServiceFacades;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Micro.OrderBLService
{
    class Program
    {
        static void Main(string[] args)
        {
            var sp = BuildService();

            Console.ReadLine();
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRepository<Order>, OrderRepoCom>();
            return services.BuildServiceProvider();
        }
    }
}
