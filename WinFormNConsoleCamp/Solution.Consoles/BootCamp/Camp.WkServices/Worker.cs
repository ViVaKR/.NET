using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Camp.WkServices;

public class Worker(ILogger<Worker> logger, IHostApplicationLifetime lifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation($"Worker running at: {DateTimeOffset.Now}", stoppingToken);
        await Task.Delay(1000, stoppingToken);
        lifetime.StopApplication();
        
    }
}
