using MassTransit;
using Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit.Consumers
{
    public class SubmitOrderConsumer : IConsumer<ISubmitOrder>
    {
        public async Task Consume(ConsumeContext<ISubmitOrder> context)
        {
            await context.Publish<IOrderSubmitted>((OrderSubmitted) new OrderSubmitted
            {
                OrderId = context.Message.OrderId,
                OrderDate = DateTime.Now
            });
        }
    }
}
