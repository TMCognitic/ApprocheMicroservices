using BStorm.Tools.CommandQuerySeparation.Queries;
using BStorm.Tools.CommandQuerySeparation.Results;
using Microsoft.AspNetCore.Mvc;
using ReadApi.Models.Entities;
using ReadApi.Models.Queries;

namespace ReadApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IQueryHandler<GetMessagesQuery, IEnumerable<Message>> _queryHandler;

        public MessageController(ILogger<MessageController> logger, IQueryHandler<GetMessagesQuery, IEnumerable<Message>> queryHandler)
        {
            _logger = logger;
            _queryHandler = queryHandler;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            try
            {
                IResult<IEnumerable<Message>> result = _queryHandler.Execute(new GetMessagesQuery());

                if(result.IsFailure)
                    return BadRequest(new { Message = result.ErrorMessage });

                return Ok(result.Content);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
