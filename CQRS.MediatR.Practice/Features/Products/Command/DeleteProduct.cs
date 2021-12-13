using CQRS.MediatR.Practice.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.MediatR.Practice.Features.Products.Command
{
    public class DeleteProduct
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _context;

            public CommandHandler(ApplicationDbContext context)
            {
                _context = context;
            } 
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = _context.products.Find(request.Id);
                if (product == null) return Unit.Value;

                _context.products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}
