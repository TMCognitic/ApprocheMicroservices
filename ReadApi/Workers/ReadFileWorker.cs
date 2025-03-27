
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
            DirectoryInfo directoryInfo = new DirectoryInfo("/shared");

            if (!directoryInfo.Exists)
            {
                _logger.LogCritical("The directory not exists");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                FileInfo[] files = directoryInfo.GetFiles();

                foreach (FileInfo file in files)
                {
                    _logger.LogInformation($"Traitement du fichier {file.Name}");
                    //Insert Db Read
                    file.Delete();
                }
                
                await Task.Delay(30000, stoppingToken);
            }
        }
    }
}
