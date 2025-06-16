namespace Camp.Demo;

public class Writer: IWriter
{
    public void Print()
    {
        Console.WriteLine(DateTime.Now);
    }
}
