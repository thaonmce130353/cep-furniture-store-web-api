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
        private readonly ApplicationDbContext _context;
        public CategoryConsumer(ILogger<CategoryConsumer> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Consume(ConsumeContext<Category> context)
        {
            Category category = context.Message;
            _context.categories.Add(category);
            _context.SaveChanges();
            await Console.Out.WriteLineAsync(context.Message.name);
            _logger.LogInformation("Category: {Value}", context.Message.name);
        }
    }
}
