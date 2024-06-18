
using System.Net;

namespace WpfSockets.Libs
{
    public class NetConf
    {
        public const int fileServerPort = 60010;
        public const int fileClientPort = 60015;
        public const int chatServerPort = 60020;
        public const int chatClientport = 60025;
        public const int infoServerPort = 60030;
        public const int infoClientPort = 60035;

        public const int backLog = 128;

        public static IPEndPoint GetEndPoint(SocketType socketType)
        {
            return socketType switch
            {
                SocketType.FileServer => new IPEndPoint(IPAddress.Any, fileServerPort),
                SocketType.FileClient => new IPEndPoint(IPAddress.Any, fileClientPort),
                SocketType.ChatServer => new IPEndPoint(IPAddress.Any, chatServerPort),
                SocketType.ChatClient => new IPEndPoint(IPAddress.Any, chatClientport),
                SocketType.InfoServer => new IPEndPoint(IPAddress.Any, infoServerPort),
                SocketType.InfoClient => new IPEndPoint(IPAddress.Any, infoClientPort),
                _ => new IPEndPoint(IPAddress.Any, fileServerPort),
            };
        }

    }
}
