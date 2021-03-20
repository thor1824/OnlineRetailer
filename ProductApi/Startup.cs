using EasyNetQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Or.Domain.Model.ServiceFacades;
using Or.Micro.Products.BackgroundServices;
using Or.Micro.Products.Data;
using Or.Micro.Products.Models;
using Or.Micro.Products.Repositories;
using Or.Micro.Products.Services;

namespace Or.Micro.Products
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
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRepository<Product>, ProductRepository>();

            // Transient
            services.AddTransient<IDbInitializer, DbInitializer>();

            // Singleton
            services.AddSingleton(RabbitHutch.CreateBus("host=rabbitmq;username=guest;password=guest"));


            // DB context
            services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("ProductDB").EnableSensitiveDataLogging());

            // Hosted Services
            services.AddHostedService<ProductListener>();

            // Controllers Etc.
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
                    var dbContext = sp.GetService<ProductContext>();
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
