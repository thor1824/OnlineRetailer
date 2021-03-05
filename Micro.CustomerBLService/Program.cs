using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Or.Domain.Model.Entities;
using Or.Domain.Model.ServiceFacades;
using RetailApi.Domain.Model.Messages;
using System;

namespace Micro.CustomerBLService
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

            var service = sp.GetService<ICustomerService>();

            #region Message Channels
            //Get(Id)
            bus.Rpc.Respond<GetBLRequest<Customer>, GetBLResponse<Customer>>(async request =>
            {
                Console.WriteLine("Get(" + request.Id.Value + ") Request Recived");
                var product = await service.GetAsync(request.Id.Value);
                return new GetBLResponse<Customer>() { Payload = product };
            });

            /*
            //GetAll
            var service = sp.GetService<IProductService>();
            bus.Rpc.Respond<GetBLRequest<IEnumerable<Customer>>, GetBLResponse<IEnumerable<Customer>>>(async request =>
            {
                Console.WriteLine("GetAllRequest Recived");
                var products = await service.GetAll();
                return new GetBLResponse<IEnumerable<Customer>>() { Payload = products };
            });
            */

            //Add(Customer)
            bus.Rpc.Respond<CreateBLRequest<Customer>, CreateBLResponse<Customer>>(async request =>
            {
                Console.WriteLine("Create Request Recived");
                var product = await service.AddAsync(request.Payload);
                return new CreateBLResponse<Customer>() { Payload = product };
            });

            //Edit(Customer)
            bus.Rpc.Respond<UpdateBLRequest<Customer>, UpdateBLResponse<Customer>>(async request =>
            {
                Console.WriteLine("Update Request Recived");
                await service.UpdateAsync(request.Payload);
                return new UpdateBLResponse<Customer>();
            });

            //Delete(Id)
            bus.Rpc.Respond<DeleteBLRequest<Customer>, DeleteBLRequest<Customer>>(async request =>
            {
                Console.WriteLine("Delete Request Recived");
                await service.DeleteAsync(request.Id);
                return new DeleteBLRequest<Customer>();
            }); 
            #endregion
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IRepository<Customer>, CustomerRepoCom>();
            services.AddSingleton(RabbitHutch.CreateBus("host=localhost"));
            return services.BuildServiceProvider();
        }
    }
}
