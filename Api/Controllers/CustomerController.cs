using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        private readonly IOptionsMonitor<AppOptions> _options;

        public CustomerController(ICustomerService service, IOptionsMonitor<AppOptions> options)
        {
            _service = service;
            _options = options;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<GetCustomerDto>> GetAllCustomers(int limit = 10, int startAt = 0)
        {
            var customers = _service.GetAllCustomers(limit, startAt);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public ActionResult<GetCustomerDto> GetCustomerById(int id)
        {
            var customer = _service.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateCustomer([FromBody] CustomerCreateDto customerDto)
        {
            _service.CreateCustomer(customerDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, [FromBody] GetCustomerDto getCustomerDto)
        {
            _service.UpdateCustomer(id, getCustomerDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            _service.DeleteCustomer(id);
            return Ok();
        }
    }
}