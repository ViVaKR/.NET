using Microsoft.Extensions.Hosting;

namespace Camp.Example;

public class Worker (IMessageWriter messageWriter): BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            messageWriter.Write($"Logged: {DateTime.Now}");
            await Task.Delay(1_000, stoppingToken);
        }
    }
}
