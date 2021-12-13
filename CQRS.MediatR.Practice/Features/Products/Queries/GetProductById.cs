using CQRS.MediatR.Practice.Data;
using CQRS.MediatR.Practice.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.MediatR.Practice.Features.Products.Queries
{
    public class GetProductById
    {
        public class Query : IRequest<Product>
        {
            public int Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Product>
        {
            private readonly ApplicationDbContext _context;

            public QueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Product> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.products.FindAsync(request.Id);
            }
        }
    }
}
