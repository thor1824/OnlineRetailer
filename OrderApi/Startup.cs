using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Or.Micro.Orders.BackgroundServices;
using Or.Micro.Orders.Data;
using Or.Micro.Orders.MessageGateways;
using Or.Micro.Orders.MessageGateways.Impl;
using Or.Micro.Orders.Repositories;
using Or.Micro.Orders.Services;

namespace Or.Micro.Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Scoped
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IProductMessageGateway, ProducMessageGateway>();
            services.AddScoped<ICustomerMessageGateway, CustomerMessageGateway>();
            services.AddScoped<IOrderMessageGateway, OrderMessageGateway>();

            // Transient
            services.AddTransient<IDbInitializer, DbInitializer>();

            // Singleton
            services.AddSingleton(RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest"));

            // DB context
            services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase("OrderDB").EnableSensitiveDataLogging());

            // Hosted Services
            services.AddHostedService<OrderListener>();


            // Controllers
            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var sp = scope.ServiceProvider;
                    var dbContext = sp.GetService<OrderContext>();
                    var dbInitializer = sp.GetService<IDbInitializer>();
                    dbInitializer.Initialize(dbContext);
                }
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
