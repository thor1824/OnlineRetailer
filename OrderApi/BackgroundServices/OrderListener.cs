using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Or.Domain.Model.Messages;
using Or.Micro.Orders.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Or.Micro.Orders.BackgroundServices
{
    public class OrderListener : BackgroundService
    {
        public IServiceProvider Provider { get; }
        public IBus Bus { get; }


        public OrderListener(IServiceProvider provider, IBus bus)
        {
            Provider = provider;
            Bus = bus;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() =>
            {
                Console.Write("Starting Listeners...");
                Bus.PubSub.SubscribeAsync<CustomerValidationVerdict>("orderApiOrderCustomVerdict", async msg =>
                    await HandleCustomerValidationVerdict(msg, stoppingToken));

                Bus.PubSub.SubscribeAsync<OutstandingBillsRequest>("orderApiOutstandingBills", async msg =>
                     await HandleOutStandingBillsRequest(msg, stoppingToken));

                Bus.PubSub.SubscribeAsync<StockValidationResponse>("orderApiOutstandingBills", async msg =>
                     await HandleStockValidationResponse(msg, stoppingToken));
                Console.WriteLine("Done!");

                lock (this)
                {
                    Monitor.Wait(this);
                }
            });

            return Task.CompletedTask;
        }

        private async Task HandleStockValidationResponse(StockValidationResponse msg, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Stock Validation of Order {msg.OrderId} received");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<IOrderService>();

                await serv.HandleStockResultAsync(msg.OrderId, msg.Verdict);
            }
        }

        private async Task HandleOutStandingBillsRequest(OutstandingBillsRequest msg, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Validating Order {msg.OrderId}: Customer");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<IOrderService>();

                bool verdict = !(await serv.HasOutStandingBill(msg.CustomerId)); // reversed to fit the format of (verdict = true) as being valid in orderprecessing
                await serv.HandleOutStandingBillsAsync(msg.OrderId, verdict);
            }
        }



        private async Task HandleCustomerValidationVerdict(CustomerValidationVerdict msg, CancellationToken stoppingToken)
        {
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<IOrderService>();

                await serv.HandleCustomerResultAsync(msg.OrderId, msg.Verdict);
            }

        }

    }
}
