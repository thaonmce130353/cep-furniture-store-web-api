using Automatonymous;
using Backend.ReceiveEndPoint.Masstransit.StateMachine.OrderServices;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ReceiveEndPoint.Masstransit.StateMachine.StateMachine
{
    public class OrderState :
    SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime? OrderDate { get; set; }
    }

    public class OrderStateMap :
    SagaClassMap<OrderState>
    {
        protected override void Configure(EntityTypeBuilder<OrderState> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
            entity.Property(x => x.OrderDate);
        }
    }

    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Initially(
            When(SubmitOrder)
                .Then(x => handleStateChange(x.Instance))
                .Publish(x => (IOrderAccepted)new OrderAccepted
                {
                    OrderId = x.Instance.CorrelationId,
                    Timestamp = DateTime.Now
                })
                .TransitionTo(Submitted),
            When(OrderSubmitted)
                .Then(x => handleStateChange(x.Instance))
                .TransitionTo(Submitted));

            During(Submitted,
                When(OrderAccepted)
                .Then(x => handleStateChange(x.Instance))
                .TransitionTo(Accepted));

            During(Accepted,
                When(OrderCompleted)
                .Then(x => handleStateChange(x.Instance))
                .TransitionTo(Completed).Finalize());

            SetCompletedWhenFinalized();

            Event(() => SubmitOrder, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderSubmitted, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderAccepted, x => x.CorrelateById(context => context.Message.OrderId));
            Event(() => OrderCompleted, x => x.CorrelateById(context => context.Message.OrderId));
        }

        public Event<ISubmitOrder> SubmitOrder { get; private set; }
        public Event<IOrderSubmitted> OrderSubmitted { get; private set; }
        public Event<IOrderAccepted> OrderAccepted { get; private set; }
        public Event<IOrderCompleted> OrderCompleted { get; private set; }

        public State Submitted { get; private set; }
        public State Accepted { get; private set; }
        public State Completed { get; private set; }

        public async void handleStateChange(OrderState instance)
        {
            Console.WriteLine("(x) State machine: [" + instance.CurrentState + "]");
            //await orderService.UpdateStatus(instance.CurrentState);
        }
    }
}
