using Microsoft.Extensions.Logging;

namespace Camp.Example;

public class LoggingMessageWriter(ILogger<LoggingMessageWriter> logger) : IMessageWriter
{
    public void Write(string message)
    {
        logger.LogInformation("Info: {Msg}", message);
    }
}
