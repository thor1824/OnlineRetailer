using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Or.Domain.Model.Messages;
using Or.Micro.Products.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Or.Micro.Products.BackgroundServices
{
    public class ProductListener : BackgroundService
    {
        public IServiceProvider Provider { get; }
        public IBus Bus { get; }


        public ProductListener(IServiceProvider provider, IBus bus)
        {
            Provider = provider;
            Bus = bus;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() =>
            {
                Console.Write("Starting Listeners...");

                Bus.PubSub.SubscribeAsync<StatusChangeMessage>("productApiOrderStatusChangeCompleted", async msg =>
                await HandleCompletedOrder(msg, stoppingToken), x => x.WithTopic("completed"));

                Bus.PubSub.SubscribeAsync<StatusChangeMessage>("productApiOrderStatusChangeCancelled", async msg =>
                    await HandleCancelledOrder(msg, stoppingToken), x => x.WithTopic("cancelled"));

                Bus.PubSub.SubscribeAsync<StatusChangeMessage>("productApiOrderStatusChangeShipped", async msg =>
                    await HandleShippedOrder(msg, stoppingToken), x => x.WithTopic("shipped"));

                Bus.PubSub.SubscribeAsync<StockValidationRequest>("productApiOrderStockValidation", async msg =>
                    await HandleStockValidation(msg, stoppingToken));
                Console.WriteLine("Done!");

                lock (this)
                {
                    Monitor.Wait(this);
                }
            });
            

            return Task.CompletedTask;
        }

        private async Task HandleStockValidation(StockValidationRequest msg, CancellationToken stoppingToken)
        {
            bool verdict = true;
            Console.WriteLine($"Validating stock for Order {msg.OrderId}");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<IProductService>();
                foreach (var line in msg.Orderlines)
                {
                    var prod = await serv.GetAsync(line.ProductId);
                    if (prod == null && prod.ProductId == 0)
                    {
                        verdict = false;
                        Console.WriteLine($"Order {msg.OrderId} Rejected: Product {line.ProductId} Does not exist");
                    }
                    if (prod.ItemsInStock < line.Quantity)
                    {

                        verdict = false;
                        Console.WriteLine($"Order {msg.OrderId} Rejected: Product {line.ProductId} quantity too low");
                    }
                }
            }
            if (verdict)
            {
                Console.WriteLine($"Order {msg.OrderId} Quantities was accepted");
            }
            await PublishStockValidationVerdict(msg.OrderId, verdict);
        }

        private async Task PublishStockValidationVerdict(int orderId, bool verdict)
        {
            var message = new StockValidationResponse { OrderId = orderId, Verdict = verdict };
            await Bus.PubSub.PublishAsync(message).ConfigureAwait(false);
        }

        private async Task HandleCompletedOrder(StatusChangeMessage msg, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Order {msg.Order.OrderId} was marked as completed: Reserving items for Order");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<IProductService>();


                foreach (var line in msg.Order.OrderLines)
                {
                    var prod = await serv.GetAsync(line.ProductId);

                    prod.ItemsInStock -= line.Quantity;
                    prod.ItemsReserved += line.Quantity;

                    await serv.UpdateAsync(prod);
                }
            }
            Console.WriteLine($"Instock Items for Order {msg.Order.OrderId} was Reserved");
        }

        private async Task HandleShippedOrder(StatusChangeMessage msg, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Order {msg.Order.OrderId} was marked as Shipped: removing from stock");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<IProductService>();

                foreach (var line in msg.Order.OrderLines)
                {
                    var prod = await serv.GetAsync(line.ProductId);

                    prod.ItemsReserved -= line.Quantity;

                    await serv.UpdateAsync(prod);
                }
            }
            Console.WriteLine($"Reserved Items for Order {msg.Order.OrderId} was Removed");
        }

        private async Task HandleCancelledOrder(StatusChangeMessage msg, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Order {msg.Order.OrderId} was marked as cancelled: Returning reserved items to stock");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<IProductService>();

                foreach (var line in msg.Order.OrderLines)
                {
                    var prod = await serv.GetAsync(line.ProductId);

                    prod.ItemsInStock += line.Quantity;
                    prod.ItemsReserved -= line.Quantity;

                    await serv.UpdateAsync(prod);
                }
            }
            Console.WriteLine($"Reserved Items for Order {msg.Order.OrderId} was returned to stock");
        }

    }
}
