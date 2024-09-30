using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service;
using Service.DTO.UpdateDto;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaperController : ControllerBase
    {
        private readonly IPaperService _service;
        private readonly IOptionsMonitor<AppOptions> _options;

        public PaperController(IPaperService service, IOptionsMonitor<AppOptions> options)
        {
            _service = service;
            _options = options;
        }

        [HttpGet]
        [Route("")]
        public ActionResult<List<GetPaperDto>> GetAllPapers(int limit = 10, int startAt = 0)
        {
            var papers = _service.GetAllPapers(limit, startAt);
            return Ok(papers);
        }

        [HttpGet("{id}")]
        public ActionResult<GetPaperDto> GetPaperById(int id)
        {
            var paper = _service.GetPaperById(id);
            if (paper == null)
            {
                return NotFound();
            }
            return Ok(paper);
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreatePaper([FromBody] CreatePaperDto createPaperDto)
        {
            _service.CreatePaper(createPaperDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePaper(int id, [FromBody] UpdatePaperDto updatePaperDto)
        {
            _service.UpdatePaper(id, updatePaperDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePaper(int id)
        {
            _service.DeletePaper(id);
            return Ok();
        }
    }
}