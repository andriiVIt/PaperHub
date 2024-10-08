using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaperPropertyController : ControllerBase
    {
        private readonly IPaperPropertyService _service;
        private readonly IOptionsMonitor<AppOptions> _options;

        public PaperPropertyController(IPaperPropertyService service, IOptionsMonitor<AppOptions> options)
        {
            _service = service;
            _options = options;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<GetPaperPropertyDto>> GetAllPaperProperties()
        {
            var paperProperties = _service.GetAllPaperProperties();
            return Ok(paperProperties);
        }

        [HttpGet("{paperId}/{propertyId}")]
        public ActionResult<GetPaperPropertyDto> GetPaperPropertyById(int paperId, int propertyId)
        {
            var paperProperty = _service.GetPaperPropertyById(paperId, propertyId);
            if (paperProperty == null)
            {
                return NotFound();
            }
            return Ok(paperProperty);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreatePaperProperty([FromBody] CreatePaperPropertyDto createPaperPropertyDto)
        {
            _service.CreatePaperProperty(createPaperPropertyDto);
            return Ok();
        }

        [HttpDelete("{paperId}/{propertyId}")]
        public IActionResult DeletePaperProperty(int paperId, int propertyId)
        {
            _service.DeletePaperProperty(paperId, propertyId);
            return Ok();
        }
    }
}