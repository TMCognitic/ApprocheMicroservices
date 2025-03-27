using Microsoft.AspNetCore.Mvc;
using IO = System.IO;

namespace WriteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                IO.File.WriteAllText($"/app/shared/{Guid.NewGuid()}.json", "Test");
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }

            
        }
    }
}
