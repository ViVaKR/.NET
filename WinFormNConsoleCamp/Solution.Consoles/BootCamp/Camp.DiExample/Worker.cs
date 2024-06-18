using Microsoft.Extensions.Hosting;

namespace Camp.DiExample;

public class Worker(ILogWriter logWriter) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logWriter.Write($"Log: {DateTime.Now}");
            await Task.Delay(1_000, stoppingToken);
        }
    }
}
