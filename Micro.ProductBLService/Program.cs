using Domane.Model;
using Domane.Model.ServiceFacades;
using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using RetailApi.Domain.Model.Messages;
using RetailApi.Domain.Model.ServiceFacades;
using System;
using System.Collections.Generic;

namespace Micro.ProductBLService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Starting service...");
            var sp = BuildService();
            using (var bus = sp.GetService<IBus>())
            {
                
                SetUpChannelWithHandlers(bus, sp);
                Console.WriteLine("Done!");
                Console.ReadLine(); 
            }
        }

        private static void SetUpChannelWithHandlers(IBus bus, ServiceProvider sp) {
            var service = sp.GetService<IProductService>();
            bus.Rpc.Respond<GetBLRequest<IEnumerable<Product>>, GetBLResponse<IEnumerable<Product>>>(async request =>
            {
                Console.WriteLine("GetAllRequest Recived");
                var products = await service.GetAllAsync();
                return new GetBLResponse<IEnumerable<Product>>() {Payload = products};
            });
            
            bus.Rpc.Respond<GetBLRequest<Product>, GetBLResponse<Product>>(async request =>
            {
                Console.WriteLine("Get("+request.Id.Value+") Request Recived");
                var product = await service.GetAsync(request.Id.Value);
                return new GetBLResponse<Product>() {Payload = product};
            });
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository<Product>, ProductRepoCom>();
            services.AddSingleton(RabbitHutch.CreateBus("host=localhost"));
            return services.BuildServiceProvider();
        }
    }
}
