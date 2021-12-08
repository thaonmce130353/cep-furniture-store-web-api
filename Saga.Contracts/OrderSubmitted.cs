using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public class OrderSubmitted : IOrderSubmitted
    {
        public Guid OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
    }

}
