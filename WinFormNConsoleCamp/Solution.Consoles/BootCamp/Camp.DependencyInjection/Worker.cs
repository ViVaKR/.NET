
﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Camp.DependencyInjection;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {     
            logger.LogInformation("Worker running at: {timer}", DateTimeOffset.Now);
            await Task.Delay(1_000, stoppingToken);
        }
    }
}
