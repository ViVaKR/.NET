
using System.Text;


namespace DelegateBasic
{
    public class Log
    {
        public void LogTextToScreen(string text, DateTime dateTime)
        {
            Console.WriteLine($"{dateTime}: {text}");
        }

        public void LogTextToFile(string text, DateTime dateTime)
        {
            string? file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt");
            using var sw = new StreamWriter(file, true, Encoding.UTF8);

            sw.WriteLine($"{dateTime}: {text}");
        }
    }
}
