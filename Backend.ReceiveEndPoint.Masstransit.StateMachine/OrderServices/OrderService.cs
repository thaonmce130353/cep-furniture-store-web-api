using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ReceiveEndPoint.Masstransit.StateMachine.OrderServices
{
    public interface IOrderService
    {
        Task<bool> UpdateStatus(string currentState);
    }

    public class OrderService : IOrderService
    {
        private readonly OrderStateDbContext _DbContext;
        public OrderService(OrderStateDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<bool> UpdateStatus(string currentState)
        {
            try
            {
                await _DbContext.AddAsync<string>(currentState);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
