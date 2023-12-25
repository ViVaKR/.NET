<<<<<<< HEAD
﻿
using Microsoft.Extensions.Hosting;

=======
﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
>>>>>>> f9cb5cf873c4c1c8af334968e7f1853ade154855
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
