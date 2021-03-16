using EasyNetQ;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Or.Micro.Customers.BackgroundServices
{
    public class CustomerSubscriper : BackgroundService
    {
        public IBus Bus { get; }


        public CustomerSubscriper(IBus bus)
        {
            Bus = bus;
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //Bus.PubSub.Subscribe<OrderStatusChangedMessage>("productApiHkCompleted",
            //    HandleOrderCompleted, x => x.WithTopic("completed"));

            // Add code to subscribe to other OrderStatusChanged events:
            // * cancelled
            // * shipped
            // * paid
            // Implement an event handler for each of these events.
            // Be careful that each subscribe has a unique subscription id
            // (this is the first parameter to the Subscribe method). If they
            // get the same subscription id, they will listen on the same
            // queue.

            // Block the thread so that it will not exit and stop subscribing.
            lock (this)
            {
                Monitor.Wait(this);
            }

        }
    }
}
