namespace Camp.DependencyInjection;

public class MessageWriter
{
    public void Write(string message)
    {
        Console.WriteLine($"{message}");
    }
}
