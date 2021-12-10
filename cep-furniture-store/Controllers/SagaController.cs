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
                await _publishEndpoint.Publish<ISubmitOrder>(new SubmitOrder
                {
                    OrderDate = DateTime.Now.ToString(),
                    OrderId = submitOrder.OrderId
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(submitOrder);
        }

        [HttpPost]
        [Route("order/complete")]
        public async Task<IActionResult> Complete([FromBody] SubmitOrder submitOrder)
        {
            try
            {
                await _publishEndpoint.Publish<IOrderCompleted>(new SubmitOrder 
                { 
                    OrderDate = DateTime.Now.ToString(),
                    OrderId = submitOrder.OrderId
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok(submitOrder);
        }
    }
}
