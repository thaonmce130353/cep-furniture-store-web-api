using Cep.Backend.ReceiveEndPoint.Masstransit.Data;
using Cep.Backend.ReceiveEndPoint.Masstransit.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Cep.Backend.ReceiveEndPoint.Masstransit.Consumers
{
    public class CategoryConsumer : IConsumer<Category>
    {
        ILogger<CategoryConsumer> _logger;
        public CategoryConsumer(ILogger<CategoryConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Category> context)
        {
            using (var _context = new ApplicationDbContext())
            {
                Category category = context.Message;
                _context.categories.Add(category);
                _context.SaveChanges();
            }
                
            await Console.Out.WriteLineAsync(context.Message.name);
            _logger.LogInformation("Category: {Value}", context.Message.name);
        }
    }
}
