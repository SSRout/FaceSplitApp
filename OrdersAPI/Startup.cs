using GreenPipes;
using MassTransit;
using MessagingQueue;
using MessagingQueue.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrdersApi.Persistence;
using OrdersAPI.Messages.Consumers;
using OrdersAPI.Services;
using System;

namespace OrdersAPI
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
            services.AddDbContext<OrdersContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OrdersContextConnection"),
                sqlServerOptionsAction: sqlOps =>
                {
                    sqlOps.EnableRetryOnFailure();
                });
            });

            services.AddHttpClient();
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddMassTransit(
               c =>
               {
                   c.AddConsumer<RegisterOrderCommandConsumer>();
               });


            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => { });
                cfg.ReceiveEndpoint(RabbitMqMassTransitConstant.RegisterOrderCommandQueue, e =>
                {
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(x => x.Interval(2, TimeSpan.FromSeconds(10)));
                    e.Consumer<RegisterOrderCommandConsumer>(provider);

                });

                cfg.ReceiveEndpoint(RabbitMqMassTransitConstant.OrderDispatchedServiceQueue, e =>
                {
                    e.PrefetchCount = 16;
                    e.UseMessageRetry(x => x.Interval(2, 100));


                    e.Consumer<OrderDispatchedEventConsumer>(provider);
                    //  EndpointConvention.Map<OrderDispatchedEvent>(e.InputAddress);

                });

                cfg.ConfigureEndpoints((IBusRegistrationContext)provider);
            }));
            services.AddSingleton<IHostedService, BusService>();

          
            services.AddCors(op =>
            {
                op.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrdersAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrdersAPI v1"));
            }

            app.UseCors("CorsPolicy");
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
