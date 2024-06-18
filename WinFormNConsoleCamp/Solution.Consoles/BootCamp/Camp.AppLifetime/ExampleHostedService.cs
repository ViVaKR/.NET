using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Camp.AppLifetime;

public class ExampleHostedService : IHostedService
{
    private readonly ILogger<ExampleHostedService> _logger;

    public ExampleHostedService(ILogger<ExampleHostedService> logger, IHostApplicationLifetime appLifetime)
    {
        _logger = logger;
        appLifetime.ApplicationStarted.Register(OnStarted);
        appLifetime.ApplicationStopping.Register(OnStopping);
        appLifetime.ApplicationStopping.Register(OnStopped);
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("1. StartAsync has been called");
        return Task.CompletedTask;
    }

    private void OnStarted()
    {
        _logger.LogInformation("2. OnStarted has been called");
    }

    private void OnStopping()
    {
        _logger.LogInformation("3. OnStopping has been called");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("4. StopAsync has been called");
        return Task.CompletedTask;
    }
    
    private void OnStopped()
    {
        _logger.LogInformation("5. OnStopped has been called");
    }

}
