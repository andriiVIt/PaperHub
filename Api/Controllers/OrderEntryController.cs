using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;
using Service.DTO.UpdateDto;

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
        public IActionResult CreateOrderEntry([FromBody] CreateOrderEntryDto createOrderEntryDto)
        {
            _service.CreateOrderEntry(createOrderEntryDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderEntry(int id, [FromBody] UpdateOrderEntryDto updateOrderEntryDto)
        {
            _service.UpdateOrderEntry(id, updateOrderEntryDto);
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