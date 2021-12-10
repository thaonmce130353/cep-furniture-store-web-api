using Backend.ReceiveEndPoint.Masstransit.StateMachine.StateMachine;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ReceiveEndPoint.Masstransit.StateMachine
{
    public class OrderStateDbContext : SagaDbContext
    {
        public OrderStateDbContext(DbContextOptions options)
        : base(options)
        {
        }
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new OrderStateMap(); }
        }
    }
}
