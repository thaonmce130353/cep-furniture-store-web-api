using Cep.Backend.ReceiveEndPoint.Masstransit.Consumers;
using Cep.Backend.ReceiveEndPoint.Masstransit.Data;
using Cep.Backend.ReceiveEndPoint.Masstransit.StateMachine;
using MassTransit;
using MassTransit.Saga;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit
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
            services.AddControllers();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CategoryConsumer>();

                var machine = new OrderStateMachine();
                var repository = new InMemorySagaRepository<OrderState>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri("amqps://elgfojpi:aIaPZPD-CRKBR0xQgMqzgST6iWBxTfx0@fox.rmq.cloudamqp.com/elgfojpi"));

                    cfg.ReceiveEndpoint("category-queue", ep =>
                    {
                        ep.ConfigureConsumer<CategoryConsumer>(provider);
                    });

                    cfg.ReceiveEndpoint("order", e =>
                    {
                        e.StateMachineSaga(machine, repository);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
