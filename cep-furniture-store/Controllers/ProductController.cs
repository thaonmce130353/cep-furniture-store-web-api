using cep_furniture_store.Data;
using cep_furniture_store.Models;
using cep_furniture_store.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cep_furniture_store.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getCurrentPage(
            [FromQuery(Name = "keyword")] string keyword = "",
            [FromQuery(Name = "categoryId")] int categoryId = 0,
            [FromQuery(Name = "page")] int p = 1,
            [FromQuery(Name = "limit")] int limit = 2,
            [FromQuery(Name = "sortBy")] string sortBy = "product",
            [FromQuery(Name = "sortValue")] int sortValue = -1)
        {
            var query = (from products in _context.products select products);
            
            //filter by category
            if (categoryId != 0)
            {
                query = query.Where(a => a.categoryId == categoryId);
            }

            //search by name
            if (keyword != "")
            {
                query = query.Where(a => a.name.Contains(keyword));
            }

            //sort
            if (sortBy.Equals("price") && sortValue == 1)
            {
                query = query.OrderBy(p => p.price);
            } 
            else if (sortBy.Equals("price") && sortValue == -1)
            {
                query = query.OrderByDescending(p => p.price);
            } 
            else if(sortBy.Equals("product") && sortValue == 1)
            {
                query = query.OrderByDescending(p => p.id);
            }

            int page = p <= 0 ? 1 : p;
            int totalProduct = query.Count();

            return Ok(new { 
                data = query.Skip((page - 1) * limit).Take(limit),
                totalProduct,
                page,
                limit,
                lastPage = Math.Ceiling((double)totalProduct / limit)
            });
        }

        [HttpGet("all")]
        public IActionResult getAll()
        {
            return Ok(_context.products);
        }

        [HttpPost("Add")]
        public IActionResult Add(Product product)
        {
            _context.products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        [HttpGet("detail")]
        public IActionResult detail([FromQuery(Name = "id")] int id)
        {
            var product = _context.products.Where(p => p.id == id).FirstOrDefault();
            return Ok(product);
        }

        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult search([FromQuery(Name = "term")] string keyword)
        {
            try
            {
                var products = _context.products.Where(p => p.name.Contains(keyword) || p.color.Contains(keyword))
                                                .Select(p => p.name).ToList();
                return Ok(products);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("search/result")]
        public IActionResult searchResult([FromQuery(Name = "keyword")] string keyword)
        {
            var products = _context.products.Where(p => p.name.Contains(keyword) || p.color.Contains(keyword));
            return Ok(products);
        }
    }
}
