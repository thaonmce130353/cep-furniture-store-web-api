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
    public class UpdateProduct
    {
        public class Command : IRequest<int>
        {
            public int id { get; set; }
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
                var product = _context.products.Find(request.id);
                if (product == null) return -1;

                product.name = request.name;
                product.price = request.price;
                product.quantity = request.quantity;
                product.material = request.material;
                product.description = request.description;
                product.dimention = request.dimention;
                product.color = request.color;
                product.categoryId = request.categoryId;
                product.status = request.status;

                await _context.SaveChangesAsync(cancellationToken);

                return product.id;
            }
        }
    }
}
