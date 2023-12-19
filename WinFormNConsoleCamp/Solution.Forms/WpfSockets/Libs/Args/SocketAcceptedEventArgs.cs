

using System.Net;
using System.Net.Sockets;

namespace WpfSockets.Libs.Args
{
    public delegate void SocketAcceptedHandler(object sender, SocketAcceptedEventArgs e);

    public class SocketAcceptedEventArgs(Socket socket) : EventArgs
    {
        public Socket AcceptedSocket { get; set; } = socket;
    }
}
