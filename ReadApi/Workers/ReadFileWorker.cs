
using System.Text.Json;
using BStorm.Tools.CommandQuerySeparation.Commands;
using ReadApi.Models.Commands;
using ReadApi.Models.Entities;
using IO = System.IO;

namespace ReadApi.Workers
{
    public class ReadFileWorker : BackgroundService
    {
        private readonly ILogger<ReadFileWorker> _logger;
        private readonly ICommandHandler<AddMessageCommand> _commandHandler;

        public ReadFileWorker(ILogger<ReadFileWorker> logger, ICommandHandler<AddMessageCommand> commandHandler)
        {
            _logger = logger;
            _commandHandler = commandHandler;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo("/app/shared");

            if (!directoryInfo.Exists)
            {
                _logger.LogCritical("The directory not exists");
                return;
            }

            _logger.LogInformation("Starting delay 60 secs");
            await Task.Delay(60000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Starting synchronization...");
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    _logger.LogInformation($"Handle file : {file.Name}");

                    try
                    {
                        _commandHandler.Execute(JsonSerializer.Deserialize<AddMessageCommand>(IO.File.ReadAllText(file.FullName)!, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!);
                        file.Delete();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }
                }
                _logger.LogInformation("Starting delay 30 secs");
                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
