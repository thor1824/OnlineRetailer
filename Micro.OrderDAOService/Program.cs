using Domain.Storage;
using Domane.Model;
using EasyNetQ;
using EasyNetQ.Custom.Serializer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetailApi.Domain.Model.Messages;
using RetailApi.Domain.Model.Messages.Specialised;
using RetailApi.Domain.Model.ServiceFacades;
using System;

namespace Micro.OrderDAOService
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
            var service = sp.GetService<IOrderRepository>();

            #region Message Channels
            //Get(Id)
            bus.Rpc.Respond<GetDaoRequest<Order>, GetDaoResponse<Order>>(async request =>
            {
                Console.WriteLine("Get(" + request.Id.Value + ") Request Recived");
                var product = await service.GetAsync(request.Id.Value);
                return new GetDaoResponse<Order>() { Payload = product };
            });

            //Add(Order)
            bus.Rpc.Respond<CreateDaoRequest<Order>, CreateDaoResponse<Order>>(async request =>
            {
                Console.WriteLine("Create Request Recived");
                var product = await service.AddAync(request.Payload);
                return new CreateDaoResponse<Order>() { Payload = product };
            });

            //GetBuCustomer(Order)
            bus.Rpc.Respond<ByCustomerDaoRequest, ByCustomerDaoResponse>(async request =>
            {
                Console.WriteLine("Get By Customer: " + request.CustomerId);
                var product = await service.GetByCustomerIdAsync(request.CustomerId);
                return new ByCustomerDaoResponse() { Payload = product };
            });

            //ChangeStatus
            bus.Rpc.Respond<UpdateDaoRequest<Order>, UpdateDaoResponse<Order>>(async request =>
            {
                Console.WriteLine("Update Request Recived");
                await service.EditAsync(request.Payload);
                return new UpdateDaoResponse<Order>();
            });
            #endregion
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOrderRepository, OrderRepository>();
            var bus = RabbitHutch.CreateBus("host=localhost", serviceRegister => serviceRegister.Register<ISerializer>(serviceProvider => new CustomSerializer()));
            services.AddSingleton(bus);

            services.AddDbContext<RetailContext>(opt => opt.UseInMemoryDatabase("RetailDB").EnableSensitiveDataLogging());
            services.AddTransient<IDbInitializer, DbInitializer>();
            return services.BuildServiceProvider();
        }
    }
}
