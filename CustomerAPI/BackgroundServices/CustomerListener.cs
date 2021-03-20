using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Or.Domain.Model.Messages;
using Or.Domain.Model.SharedModels;
using Or.Micro.Customers.Models;
using Or.Micro.Customers.Service;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Or.Micro.Customers.BackgroundServices
{
    public class CustomerListener : BackgroundService
    {
        public IServiceProvider Provider { get; }
        public IBus Bus { get; }


        public CustomerListener(IServiceProvider provider, IBus bus)
        {
            Provider = provider;
            Bus = bus;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // notify
            Task.Run(() =>
            {
                Console.Write("Starting Listeners...");
                Bus.PubSub.SubscribeAsync<StatusChangeMessage>("customerApiOrderCompleteNotify", async msg =>
                    await HandleOrderCompletedNotify(msg, stoppingToken), x => x.WithTopic("completed").WithTopic("shipped").WithTopic("paid").WithTopic("cancelled").WithTopic("rejected"));

                Bus.PubSub.SubscribeAsync<CustomerValidationMessage>("customerApiCustomeValidation", async msg =>
                    await HandleCustomerValidation(msg, stoppingToken));

                Bus.Rpc.RespondAsync<CustomerExistsRequest, CustomerExistsResponse>(HandleCustomersExistsRequest);
                Console.WriteLine("Done!");

                lock (this)
                {
                    Monitor.Wait(this);
                }
            });

            return Task.CompletedTask;
        }

        private async Task<CustomerExistsResponse> HandleCustomersExistsRequest(CustomerExistsRequest request)
        {
            Customer cust;
            Console.WriteLine($"Validationg if Customer {request.CustomerId}, exists");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<ICustomerService>();
                cust = await serv.GetAsync(request.CustomerId);
            }
            var verdict = cust != null;
            Console.WriteLine($"Verdict: Customer {request.CustomerId} exists, {verdict}");

            return new CustomerExistsResponse { Verdict = verdict };
        }


        private async Task HandleOrderCompletedNotify(StatusChangeMessage msg, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Sending notice to Customer {msg.Order.CustomerId}");
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<ICustomerService>();
                var cust = await serv.GetAsync(msg.Order.CustomerId);

                var to = cust.Email;
                string content = "";
                switch (msg.Order.Status)
                {
                    case OrderStatusDto.Completed:
                        content = $"Order {msg.Order.CustomerId} Accepted";
                        break;
                    case OrderStatusDto.Rejected:
                        content = $"Order {msg.Order.CustomerId} Rejected";
                        break;
                    case OrderStatusDto.Cancelled:
                        content = $"Order {msg.Order.CustomerId} was cancelled";
                        break;
                    case OrderStatusDto.Shipped:
                        content = $"Order {msg.Order.CustomerId} was Shipped";
                        break;
                    case OrderStatusDto.Paid:
                        content = $"Payment for order {msg.Order.OrderId}, recieved";
                        break;
                    default:
                        throw new Exception("Unwanted status was recieved");
                }
                var sb = new StringBuilder();
                sb.Append($"\nSending Simulated email")
                    .Append($"\nTo\t: {to}").Append($"From\t: noreply@OnlineRetailer.dk")
                    .Append("\n----------------------------------------\n")
                    .Append(content)
                    .Append("\n----------------------------------------");
                Console.WriteLine(sb.ToString());
            }
        }

        private async Task HandleCustomerValidation(CustomerValidationMessage msg, CancellationToken stoppingToken)
        {
            Console.WriteLine($"Validationg Customer {msg.CustomerId}");

            bool v = false;
            using (var scope = Provider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var serv = services.GetService<ICustomerService>();

                var cust = await serv.GetAsync(msg.CustomerId);

                // Check Credit standing
                v = cust.CreditStanding > 0;
            }
            Console.WriteLine($"Customer {msg.CustomerId} was " + (v ? "Accepted" : "Rejected"));

            await PublisCustomerValidationResultAsync(msg.OrderId, v);
        }

        private async Task PublisCustomerValidationResultAsync(int orderId, bool verdict)
        {
            var message = new CustomerValidationVerdict { OrderId = orderId, Verdict = verdict };
            await Bus.PubSub.PublishAsync(message).ConfigureAwait(false);
        }

    }
}
