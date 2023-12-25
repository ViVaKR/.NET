using Microsoft.Extensions.Hosting;

namespace Camp.AppLifetime;

public class Worker : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine($"{DateTime.Now}");
            await Task.Delay(1_000, stoppingToken);
        }
    }
}
