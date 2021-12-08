using MassTransit;
using Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit.Consumers
{
    public class OrderCompletedConsumer : IConsumer<IOrderCompleted>
    {
        public async Task Consume(ConsumeContext<IOrderCompleted> context)
        {
            await Console.Out.WriteLineAsync("Process is ending...." + context.Message.OrderId);
        }
    }
}
