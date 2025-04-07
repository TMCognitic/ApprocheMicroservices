using BStorm.Tools.CommandQuerySeparation.Commands;
using Microsoft.AspNetCore.Mvc;
using WriteApi.Models.Commands;
using WriteApi.Models.Dtos;
using IO = System.IO;
using ICqsResult = BStorm.Tools.CommandQuerySeparation.Results.IResult;
using System.Text.Json;

namespace WriteApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly ICommandHandler<CreateMessageCommand> _commandHandler;

        public MessageController(ILogger<MessageController> logger, ICommandHandler<CreateMessageCommand> commandHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        [HttpPost]
        public IActionResult Post(CreateMessageDto dto)
        {
            try
            {
                CreateMessageCommand command = new CreateMessageCommand(dto.Nom, dto.Message);

                ICqsResult result = _commandHandler.Execute(command);

                if (result.IsFailure)
                {
                    return BadRequest(new { Message = result.ErrorMessage });
                }

                IO.File.WriteAllText($"/app/shared/{command.Uid}.json", JsonSerializer.Serialize(command, new JsonSerializerOptions() { WriteIndented = true }));
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }

            
        }
    }
}
