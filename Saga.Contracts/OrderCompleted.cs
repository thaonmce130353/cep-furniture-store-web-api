using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public class OrderCompleted : IOrderCompleted
    {
        public Guid OrderId { get; set; }
    }
}
