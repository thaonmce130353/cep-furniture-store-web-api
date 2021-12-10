using Backend.ReceiveEndPoint.Masstransit.StateMachine.OrderServices;
using Backend.ReceiveEndPoint.Masstransit.StateMachine.StateMachine;
using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.JobService;
using MassTransit.JobService.Components.StateMachines;
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

namespace Backend.ReceiveEndPoint.Masstransit.StateMachine
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
            string sqlConnectionString = Configuration.GetConnectionString("WebApiDatabase");

            services.AddMassTransit(cfg =>
            {

                //state machine with entity framework core
                cfg.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .EntityFrameworkRepository(r =>
                    {
                        r.ConcurrencyMode = ConcurrencyMode.Pessimistic;

                        r.AddDbContext<DbContext, OrderStateDbContext>((provider, builder) =>
                        {
                            builder.UseSqlServer(sqlConnectionString, m =>
                            {
                                m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                                m.MigrationsHistoryTable($"__{nameof(OrderStateDbContext)}");
                            });
                        });
                    });

                var machine = new OrderStateMachine();
                var repository = new InMemorySagaRepository<OrderState>();

                cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri("amqps://elgfojpi:aIaPZPD-CRKBR0xQgMqzgST6iWBxTfx0@fox.rmq.cloudamqp.com/elgfojpi"));

                    cfg.ReceiveEndpoint("order", e =>
                    {
                        e.StateMachineSaga(machine, repository);
                    });
                }));
            });
            

            services.AddMassTransitHostedService();
            services.AddControllers();
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
