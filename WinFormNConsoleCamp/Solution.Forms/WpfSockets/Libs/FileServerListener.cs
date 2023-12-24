using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WpfSockets.Libs.Args;

namespace WpfSockets.Libs
{
    public class FileServerListener(SocketType socketType) : BaseSocket(socketType)
    {
        public event SocketAcceptedHandler? SocketAccepted;

        public override void Start()
        {
            base.Start();

            if (IsRunning) return;
            IsRunning = true;

            VivSocket?.BeginAccept(AcceptCallback, null);

        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                if (VivSocket == null) return;
                Socket client = VivSocket.EndAccept(ar);
                ClientSockets.Add(client);
                SocketAccepted?.Invoke(this, new SocketAcceptedEventArgs(client));
            }
            catch
            {

                Stop();
            }
        }

        private void Stop()
        {
            if (VivSocket == null || !IsRunning) return;
            IsRunning = false;
            VivSocket.Close();
            VivSocket = null;

        }
    }
}
