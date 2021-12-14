using cep_furniture_store.Data;
using cep_furniture_store.Helpers;
using cfs = cep_furniture_store.Models;
using cbrm = Cep.Backend.ReceiveEndPoint.Masstransit.Models;
using cep_furniture_store.RabbitMQ;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public CategoryController(ApplicationDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public IEnumerable<cfs.Category> getAllCategory()
        {
            return _context.categories.ToList();
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(cfs.Category category)
        {
            try
            {
                cbrm.Category categoryDb = new cbrm.Category 
                { 
                    name = category.name, 
                    image = category.image, 
                    id = category.id, 
                    status = category.status
                };
                await _publishEndpoint.Publish<cbrm.Category>(categoryDb);

                //RabbitMQClient client = new RabbitMQClient();
                //client.SendMessage(category);
                //client.Close();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
            return Ok(category);
        }
    }
}
