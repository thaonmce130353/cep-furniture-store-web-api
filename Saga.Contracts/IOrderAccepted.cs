using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public interface IOrderAccepted
    {
        Guid OrderId { get; }
        string Timestamp { get; }
    }
}
