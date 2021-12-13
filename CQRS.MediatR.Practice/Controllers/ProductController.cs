using CQRS.MediatR.Practice.Features.Products.Command;
using CQRS.MediatR.Practice.Features.Products.Queries;
using CQRS.MediatR.Practice.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.MediatR.Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts() => await _mediator.Send(new GetAllProducts.Query());

        [HttpGet("{productId}")]
        public async Task<Product> GetProduct(int productId) => await _mediator.Send(new GetProductById.Query {Id = productId});

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] CreateNewProduct.Command product) 
        {
            var createdProductId = await _mediator.Send(product);
            return CreatedAtAction(nameof(GetProduct), new { productId = createdProductId }, null);
        }

        [HttpDelete("{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            await _mediator.Send(new DeleteProduct.Command { Id = productId});
            return NoContent();
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> UpdateProduct(int productId, UpdateProduct.Command newProduct)
        {
            newProduct.id = productId;
            await _mediator.Send(newProduct);
            return Ok(newProduct);
        }
    }
}
