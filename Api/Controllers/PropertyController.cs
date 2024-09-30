using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _service;
        private readonly IOptionsMonitor<AppOptions> _options;

        public PropertyController(IPropertyService service, IOptionsMonitor<AppOptions> options)
        {
            _service = service;
            _options = options;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<GetPropertyDto>> GetAllProperties()
        {
            var properties = _service.GetAllProperties();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public ActionResult<GetPropertyDto> GetPropertyById(int id)
        {
            var property = _service.GetPropertyById(id);
            if (property == null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateProperty([FromBody] PropertyCreateDto propertyDto)
        {
            _service.CreateProperty(propertyDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProperty(int id, [FromBody] GetPropertyDto getPropertyDto)
        {
            _service.UpdateProperty(id, getPropertyDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProperty(int id)
        {
            _service.DeleteProperty(id);
            return Ok();
        }
    }
}