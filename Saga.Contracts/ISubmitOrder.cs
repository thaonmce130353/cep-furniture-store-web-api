
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public interface ISubmitOrder
    {
        Guid OrderId { get; }
        string OrderDate { get; }
    }
}
