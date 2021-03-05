using EasyNetQ;
using EasyNetQ.Custom.Serializer;
using Microsoft.Extensions.DependencyInjection;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using RetailApi.Domain.Model.Messages.Specialised;
using RetailApi.Micro.OrderBLService.ServiceChannels;
using System;

namespace Micro.OrderBLService
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

        private static void SetUpChannelWithHandlers(IBus bus, ServiceProvider sp)
        {
            var service = sp.GetService<IOrderService>();

            #region Message Channels
            //Get(Id)
            bus.Rpc.Respond<GetBLRequest<Order>, GetBLResponse<Order>>(async request =>
            {
                Console.WriteLine("Get(" + request.Id.Value + ") Request Recived");
                var product = await service.GetAsync(request.Id.Value);
                return new GetBLResponse<Order>() { Payload = product };
            });

            //Add(Order)
            bus.Rpc.Respond<CreateBLRequest<Order>, CreateBLResponse<Order>>(async request =>
            {
                Console.WriteLine("Create Request Recived");
                var product = await service.AddAsync(request.Payload);
                return new CreateBLResponse<Order>() { Payload = product };
            });

            //GetBuCustomer(Order)
            bus.Rpc.Respond<ByCustomerBLRequest, ByCustomerBLResponse>(async request =>
            {
                Console.WriteLine("Get By Customer: " + request.CustomerId);
                var product = await service.GetAllByCustomerAsync(request.CustomerId);
                return new ByCustomerBLResponse() { Payload = product };
            });

            //ChangeStatus
            bus.Rpc.Respond<UpdateBLRequest<Order>, UpdateBLResponse<Order>>(async request =>
            {
                Console.WriteLine("Update Request Recived");
                await service.ChangeStatusAsync(request.Payload.OrderId.Value, request.Payload.Status.Value);
                return new UpdateBLResponse<Order>();
            });
            #endregion
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerCommunicator>();
            services.AddScoped<IProductService, ProductCommunicator>();
            services.AddScoped<IOrderRepository, OrderRepoCom>();
            var bus = RabbitHutch.CreateBus("host=localhost", serviceRegister => serviceRegister.Register<ISerializer>(serviceProvider => new CustomSerializer()));
            services.AddSingleton(bus);
            return services.BuildServiceProvider();
        }
    }
}
