using MassTransit;
using Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit.Consumers
{
    public class OrderSubmittedConsumer : IConsumer<IOrderSubmitted>
    {
        public async Task Consume(ConsumeContext<IOrderSubmitted> context)
        {
            await context.Publish<IOrderAccepted>(new
            {
                context.Message.OrderId
            });
        }
    }
}
