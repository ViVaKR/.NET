using System.Net;
using System.Net.Sockets;

namespace WpfSockets.Libs
{
    public class BaseSocket(SocketType socketType)
    {
        public Socket? VivSocket { get; set; }

        public IPEndPoint VivIPEndPoint { get; set; } = NetConf.GetEndPoint(socketType);

        public bool IsRunning { get; set; }

        public List<Socket> ClientSockets { get; set; } = [];

        public virtual void Start()
        {
            VivSocket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, ProtocolType.Tcp);
            VivSocket.Bind(VivIPEndPoint);
            VivSocket.Listen(NetConf.backLog);
        }

    }
}
