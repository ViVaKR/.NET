namespace Camp.DiExample;

public class LogWriter : ILogWriter
{
    public void Write(string log)
    {
        Console.WriteLine($"{log}");    
    }
}
