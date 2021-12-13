using CQRS.MediatR.Practice.Data;
using CQRS.MediatR.Practice.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.MediatR.Practice.Features.Products.Command
{
    public class CreateNewProduct
    {
        public class Command : IRequest<int>
        {
            public string name { get; set; }
            public double price { get; set; }
            public string material { get; set; }
            public string color { get; set; }
            public string dimention { get; set; }
            public string description { get; set; }
            public int quantity { get; set; }
            public int status { get; set; }
            public int categoryId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly ApplicationDbContext _context;

            public CommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = new Product
                {
                    name = request.name,
                    price = request.price,
                    material = request.material,
                    color = request.color,
                    dimention = request.dimention,
                    description = request.description,
                    quantity = request.quantity,
                    status = request.status,
                    categoryId = request.categoryId
                };
                await _context.AddAsync(product, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return product.id;
            }
        }
    }
}
