
namespace DependenceInjector
{
    
    public class Simple 
    {
        INotificationAction? _notice;

        public void Notify(INotificationAction notice, string message)
        {
            _notice = notice;
            _notice.ActOnNotification(message);
        }

    }
}
