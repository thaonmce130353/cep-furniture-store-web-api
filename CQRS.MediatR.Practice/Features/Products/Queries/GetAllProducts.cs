using CQRS.MediatR.Practice.Data;
using CQRS.MediatR.Practice.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.MediatR.Practice.Features.Products.Queries
{
    public class GetAllProducts
    {
        public class Query : IRequest<IEnumerable<Product>> { }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Product>>
        {
            private readonly ApplicationDbContext _context;

            public QueryHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Product>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.products.ToListAsync(cancellationToken);
            }
        }
    }
}
