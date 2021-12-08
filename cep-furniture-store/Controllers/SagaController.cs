using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saga.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cep_furniture_store.Controllers
{
    [Route("api/state-machine")]
    [ApiController]
    public class SagaController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public SagaController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost]
        [Route("order")]
        public async Task<IActionResult> Add([FromBody] SubmitOrder submitOrder)
        {
            try
            {
                await _publishEndpoint.Publish<ISubmitOrder>(submitOrder);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(submitOrder);
        }
    }
}
