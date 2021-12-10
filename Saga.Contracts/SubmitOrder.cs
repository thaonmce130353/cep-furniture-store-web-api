
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public class SubmitOrder : ISubmitOrder
    {
        public Guid OrderId { get; set; }
        public string OrderDate { get; set; }
        public string status;
    }
}
