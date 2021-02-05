using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrdersApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderReopository;

        public OrdersController(IOrderRepository orderReopository)
        {
            _orderReopository = orderReopository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var data = await _orderReopository.GetOrdersAsync();
            return Ok(data);
        }

        [HttpGet]
        [Route("{orderId}",Name ="GetOrderById")]
        public async Task<IActionResult> GetOrderById(string orderId)
        {
            var order = await _orderReopository.GetOrderAsync(Guid.Parse(orderId));
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
