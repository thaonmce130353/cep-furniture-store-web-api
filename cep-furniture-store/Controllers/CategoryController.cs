using cep_furniture_store.Data;
using cep_furniture_store.Helpers;
using cep_furniture_store.Models;
using cep_furniture_store.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
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

        public CategoryController(ApplicationDbContext context)
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
            try
            {
                RabbitMQClient client = new RabbitMQClient();
                client.SendMessage(category);
                client.Close();
            }
            catch (Exception)
            {
                return null;
            }
            
            return Ok(category);
        }
    }
}
