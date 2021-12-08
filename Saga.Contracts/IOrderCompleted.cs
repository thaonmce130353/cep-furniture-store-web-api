using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public interface IOrderCompleted
    {
        Guid OrderId { get; }
    }
}
