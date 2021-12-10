using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Saga.Components.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Components
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
