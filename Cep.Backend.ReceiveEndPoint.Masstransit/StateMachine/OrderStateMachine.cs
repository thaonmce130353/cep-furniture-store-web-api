using Automatonymous;
using Cep.Backend.ReceiveEndPoint.Masstransit.Data;
using Cep.Backend.ReceiveEndPoint.Masstransit.Models;
using Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit.StateMachine
{
    public class OrderState :
    SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public string OrderDate { get; set; }
    }

    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Initially(
            When(SubmitOrder)
                .Then(x => x.Instance.OrderDate = x.Data.OrderDate)
                .Publish(x => (IOrderAccepted)new OrderAccepted
                {
                    OrderId = x.Instance.CorrelationId,
                    Timestamp = DateTime.Now.ToString()
                })
                .TransitionTo(Submitted));

            During(Submitted,
                When(OrderAccepted)
                .Then(x => handleStateChange(x.Instance))
                .TransitionTo(Accepted));

            During(Accepted,
                When(OrderCompleted)
                .Then(x => handleStateChange(x.Instance))
                .TransitionTo(Completed)
                .Then(x => handleStateChange(x.Instance))
                .Finalize());

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

            await using (var _context = new ApplicationDbContext())
            {
                
                if (instance.CurrentState == "Submitted")
                {
                    _context.orders.Add(new Order
                    {
                        OrderDay = instance.OrderDate,
                        OrderId = instance.CorrelationId,
                        status = instance.CurrentState
                    });
                    _context.SaveChanges();
                }
                else
                {
                    var order = _context.orders.First(a => a.OrderId == instance.CorrelationId);
                    order.status = instance.CurrentState;
                    _context.orders.Attach(order);
                    _context.Entry(order).Property(x => x.status).IsModified = true;
                    if (instance.CurrentState == "Completed")
                    {
                        _context.SaveChanges();
                    }
                }
            }
            Console.WriteLine("(x) State machine: [" + instance.CurrentState + "]");
        }
    }
}