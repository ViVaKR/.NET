
namespace DependenceInjector
{
    public class EventLogWriter : INotificationAction
    {
        public void ActOnNotification(string message)
        {
            Console.WriteLine($"Click on the Bell icon to get notifications {message}");
        }
    }
}
