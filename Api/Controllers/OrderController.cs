using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IOptionsMonitor<AppOptions> _options;

        public OrderController(IOrderService service, IOptionsMonitor<AppOptions> options)
        {
            _service = service;
            _options = options;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<OrderDto>> GetAllOrders(int limit = 10, int startAt = 0)
        {
            var orders = _service.GetAllOrders(limit, startAt);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetOrderById(int id)
        {
            var order = _service.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            _service.CreateOrder(orderDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            _service.UpdateOrder(id, orderDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _service.DeleteOrder(id);
            return Ok();
        }
    }
}