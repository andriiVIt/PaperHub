using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderEntryController : ControllerBase
    {
        private readonly IOrderEntryService _service;
        private readonly IOptionsMonitor<AppOptions> _options;

        public OrderEntryController(IOrderEntryService service, IOptionsMonitor<AppOptions> options)
        {
            _service = service;
            _options = options;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<GetOrderEntryDto>> GetAllOrderEntries()
        {
            var orderEntries = _service.GetAllOrderEntries();
            return Ok(orderEntries);
        }

        [HttpGet("{id}")]
        public ActionResult<GetOrderEntryDto> GetOrderEntryById(int id)
        {
            var orderEntry = _service.GetOrderEntryById(id);
            if (orderEntry == null)
            {
                return NotFound();
            }
            return Ok(orderEntry);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateOrderEntry([FromBody] OrderEntryCreateDto orderEntryDto)
        {
            _service.CreateOrderEntry(orderEntryDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderEntry(int id, [FromBody] GetOrderEntryDto getOrderEntryDto)
        {
            _service.UpdateOrderEntry(id, getOrderEntryDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderEntry(int id)
        {
            _service.DeleteOrderEntry(id);
            return Ok();
        }
    }
}