using Domane.Model.ServiceFacades;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderApi.Requester;

namespace OrderApi
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
            // In-memory database:
            // services.AddDbContext<OrderApiContext>(opt => opt.UseInMemoryDatabase("OrdersDb"));

            // Register repositories for dependency injection
            services.AddScoped<IOrderService, OrderCommunicator>();

            // Register database initializer for dependency injection
            //services.AddTransient<IDbInitializer, DbInitializer>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //// Initialize the database
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    // Initialize the database
            //    var services = scope.ServiceProvider;
            //    var dbContext = services.GetService<OrderApiContext>();
            //    var dbInitializer = services.GetService<IDbInitializer>();
            //    dbInitializer.Initialize(dbContext);
            //}

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
