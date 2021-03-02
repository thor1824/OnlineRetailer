using Domain.Storage;
using Domane.Model;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetailApi.Domain.Model.Messages;
using RetailApi.Domain.Model.ServiceFacades;
using System;

namespace Micro.CustomerDAOService
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
            var service = sp.GetService<IRepository<Customer>>();
            //Get(Id)
            bus.Rpc.Respond<GetDaoRequest<Customer>, GetDaoResponse<Customer>>(async request =>
            {
                Console.WriteLine("Get(" + request.Id.Value + ") Request Recived");
                var product = await service.GetAsync(request.Id.Value);
                return new GetDaoResponse<Customer>() { Payload = product };
            });

            /*
            //GetAll
            var service = sp.GetService<IProductService>();
            bus.Rpc.Respond<GetDaoRequest<IEnumeraDaoe<Customer>>, GetDaoResponse<IEnumeraDaoe<Customer>>>(async request =>
            {
                Console.WriteLine("GetAllRequest Recived");
                var products = await service.GetAll();
                return new GetDaoResponse<IEnumeraDaoe<Customer>>() { Payload = products };
            });
            */

            //Add(Customer)
            bus.Rpc.Respond<CreateDaoRequest<Customer>, CreateDaoResponse<Customer>>(async request =>
            {
                Console.WriteLine("Create Request Recived");
                var product = await service.AddAync(request.Payload);
                return new CreateDaoResponse<Customer>() { Payload = product };
            });

            //Edit(Customer)
            bus.Rpc.Respond<UpdateDaoRequest<Customer>, UpdateDaoResponse<Customer>>(async request =>
            {
                Console.WriteLine("Update Request Recived");
                await service.EditAsync(request.Payload);
                return new UpdateDaoResponse<Customer>();
            });

            //Delete(Id)
            bus.Rpc.Respond<DeleteDaoRequest<Customer>, DeleteDaoRequest<Customer>>(async request =>
            {
                Console.WriteLine("Delete Request Recived");
                await service.RemoveAsync(request.Id);
                return new DeleteDaoRequest<Customer>();
            });
        }

        private static ServiceProvider BuildService()
        {
            var services = new ServiceCollection();

            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddDbContext<RetailContext>(opt => opt.UseInMemoryDatabase("RetailDB"));
            services.AddTransient<IDbInitializer, DbInitializer>();
            services.AddSingleton(RabbitHutch.CreateBus("host=localhost"));

            return services.BuildServiceProvider();
        }
    }
}
