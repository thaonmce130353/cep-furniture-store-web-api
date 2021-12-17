using cep_furniture_store.Data;
using cep_furniture_store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cep_furniture_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController( ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Category> getAllCategory()
        {
            return _context.categories.ToList();
        }

        [HttpPost("Add")]
        public IActionResult Add(Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges();
            return Ok(category);
        }
    }
}
