using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Or.Domain.Model.ServiceFacades;
using Or.Domain.Storage;
using Or.Micro.Orders.Repositories;
using Or.Micro.Orders.ServiceChannels;
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
            services.AddHttpClient();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<ICustomerService, CustomerHttpChannel>();
            services.AddScoped<IProductService, ProductHttpChannel>();

            services.AddDbContext<RetailContext>(opt => opt.UseInMemoryDatabase("RetailDB").EnableSensitiveDataLogging());
            services.AddTransient<IDbInitializer, DbInitializer>();
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
                    var dbContext = sp.GetService<RetailContext>();
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
