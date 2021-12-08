using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public class OrderAccepted : IOrderAccepted 
    {
        public Guid OrderId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
