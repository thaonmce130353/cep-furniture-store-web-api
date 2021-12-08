using MassTransit;
using Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit.Consumers
{
    public class OrderAcceptedConsumer : IConsumer<IOrderAccepted>
    {
        public async Task Consume(ConsumeContext<IOrderAccepted> context)
        {
            await context.Publish<IOrderCompleted>(new
            {
                context.Message.OrderId
            });
        }
    }
}
