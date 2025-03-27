
namespace ReadApi.Workers
{
    public class ReadFileWorker : BackgroundService
    {
        private readonly ILogger<ReadFileWorker> _logger;

        public ReadFileWorker(ILogger<ReadFileWorker> logger)
        {
            _logger = logger;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
