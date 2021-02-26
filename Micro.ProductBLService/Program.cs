using Domain.Model.ServiceFacades;
using Domane.Model;
using Domane.Model.ServiceFacades;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Micro.ProductBLService
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
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository<Product>, ProductRepoCom>();
            return services.BuildServiceProvider();
        }
    }
}
