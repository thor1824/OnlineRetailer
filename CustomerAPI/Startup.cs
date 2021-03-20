using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Or.Domain.Model.ServiceFacades;
using Or.Micro.Customers.BackgroundServices;
using Or.Micro.Customers.Data;
using Or.Micro.Customers.Models;
using Or.Micro.Customers.Repositories;
using Or.Micro.Customers.Service;

namespace Or.Micro.Customers
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Scoped
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();

            // Transient
            services.AddTransient<IDbInitializer, DbInitializer>();

            // Singleton
            services.AddSingleton(RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest"));

            // DB context
            services.AddDbContext<CustomerContext>(opt => opt.UseInMemoryDatabase("CustomerDB").EnableSensitiveDataLogging());

            // Hosted Services
            services.AddHostedService<CustomerListener>();

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
                    var dbContext = sp.GetService<CustomerContext>();
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
