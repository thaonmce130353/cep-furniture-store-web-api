using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Contracts
{
    public interface IOrderSubmitted
    {
        Guid OrderId { get; }
        DateTime? OrderDate { get; }
    }

}
