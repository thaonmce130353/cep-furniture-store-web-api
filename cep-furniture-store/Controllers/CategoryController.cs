using cep_furniture_store.Data;
using cep_furniture_store.Helpers;
using cep_furniture_store.HubConfig;
using cep_furniture_store.Models;
using cep_furniture_store.TimerFeatures;
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

        private IHubContext<NotifyHub> _hub;
        private readonly IRedisClientsManager _redisClientsManager;

        public CategoryController(
            ApplicationDbContext context, 
            IHubContext<NotifyHub> hub, 
            IRedisClientsManager redisClientsManager)
        {
            _context = context;
            _hub = hub;
            _redisClientsManager = redisClientsManager;
        }

        [HttpGet]
        public IEnumerable<Category> getAllCategory()
        {
            return _context.categories.ToList();
        }

        [HttpPost("Add")]
        public IActionResult Add(Category category)
        {
            //_context.categories.Add(category);
            //_context.SaveChanges();
            using (IRedisClient redis = _redisClientsManager.GetClient())
            {
                var categories = _context.categories.ToList();
                _hub.Clients.All.SendAsync("ReceiveMessage", categories.Count() + "");
            }
            return Ok(category);
        }
    }
}
