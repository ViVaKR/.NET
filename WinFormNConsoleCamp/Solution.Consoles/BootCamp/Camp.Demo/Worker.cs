using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Camp.Demo;

public class Worker(IWriter writer) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        writer.Print();
        await Task.CompletedTask;
    }
}
