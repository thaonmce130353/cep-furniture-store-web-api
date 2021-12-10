using Automatonymous;
using System;
using Saga.Contracts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Saga.Components.StateMachine
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

            // If using Optimistic concurrency, otherwise remove this property
            //entity.Property(x => x.RowVersion).IsRowVersion();
        }
    }

    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Initially(
            When(SubmitOrder)
                .Then(x => test("Initially"))
                .Publish(x => (IOrderAccepted)new OrderAccepted
                {
                    OrderId = x.Instance.CorrelationId,
                    Timestamp = DateTime.Now
                })
                .TransitionTo(Submitted),
            When(OrderSubmitted)
                .Then(x => test("Submitted"))
                .TransitionTo(Submitted));

            During(Submitted,
                When(OrderAccepted)
                .Then(x => test("Accepted"))
                .TransitionTo(Accepted));

            During(Accepted,
                When(OrderCompleted)
                .Then(x => test("Completed"))
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

        public void test(string state)
        {
            Console.WriteLine("(x) State machine: [" + state + "]");
        }
    }
}
