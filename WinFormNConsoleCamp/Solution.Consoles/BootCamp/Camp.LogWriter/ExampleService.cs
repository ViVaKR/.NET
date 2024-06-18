using Microsoft.Extensions.Logging;

namespace Camp.LogWriter;

public class ExampleService(ILogger<ExampleService> logger)
{
    public void DoSomeWork(int x, int y)
    {
        Console.WriteLine($"Log Information");
        logger.LogInformation("Called. x = {x}, y = {y}", x, y);
    }
}
