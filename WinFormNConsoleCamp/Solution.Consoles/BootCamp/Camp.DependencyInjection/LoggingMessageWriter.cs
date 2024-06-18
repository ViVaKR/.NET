using Microsoft.Extensions.Logging;

namespace Camp.DependencyInjection;

public class LoggingMessageWriter(ILogger<LoggingMessageWriter> logger) : IMessageWriter
{
    public void Write(string message)
    {
        logger.LogInformation("Info: {msg}", message);
    }
}
