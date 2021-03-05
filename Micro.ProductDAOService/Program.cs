using EasyNetQ;
using Micro.ProductDAOService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using Or.Domain.Storage;
using RetailApi.Domain.Model.Messages;
using System;
using System.Collections.Generic;

namespace ProductDAOService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("starting service...");
            var sp = BuildService();

            var dbContext = sp.GetService<RetailContext>();
            var dbInitializer = sp.GetService<IDbInitializer>();
            dbInitializer.Initialize(dbContext);
            using (var bus = sp.GetService<IBus>())
            {

                SetUpChannelWithHandlers(bus, sp);
                Console.WriteLine("Done!");
                Console.ReadLine();
            }
        }

        private static void SetUpChannelWithHandlers(IBus bus, ServiceProvider sp)
        {
            var service = sp.GetService<IRepository<Product>>();

            //Get(Id)
            bus.Rpc.Respond<GetDaoRequest<Product>, GetDaoResponse<Product>>(async request =>
            {
                Console.WriteLine("Dao Product Service Get(" + request.Id + ") Request Recived");
                var product = await service.GetAsync(request.Id.Value);

                return new GetDaoResponse<Product>() { Payload = product };
            });

            //GetAll
            bus.Rpc.Respond<GetDaoRequest<IEnumerable<Product>>, GetDaoResponse<IEnumerable<Product>>>(async request =>
            {
                Console.WriteLine("Dao Product Service GetAll Request Recived");
                var products = await service.GetAllAsync();
                return new GetDaoResponse<IEnumerable<Product>>() { Payload = products };
            });
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddDbContext<RetailContext>(opt => opt.UseInMemoryDatabase("RetailDB").EnableSensitiveDataLogging());
            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddSingleton(RabbitHutch.CreateBus("host=localhost"));

            return services.BuildServiceProvider();
        }
    }
}
