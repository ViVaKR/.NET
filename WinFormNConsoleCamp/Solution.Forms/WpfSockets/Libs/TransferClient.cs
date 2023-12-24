
using System.Net;
using System.Net.Sockets;

namespace WpfSockets.Libs
{
    public delegate void TransferEventHandler(object sender, TransferQueue queue);

    public delegate void ConnectCallback(object sender, string error);

    public class TransferClient(ConnectCallback callback)
    {
        public event TransferEventHandler? Queued;
        public event TransferEventHandler? ProgressChanged;
        public event TransferEventHandler? Stopped;
        public event TransferEventHandler? Complete;
        public event EventHandler? Disconnected;

        public Socket? socket;
        private readonly ConnectCallback connectCallBack = callback;
        public byte[] buffer = new byte[8192];
        public bool isClosed = true;
        private IPEndPoint? remoteEndPoint;
        public string? saveFloder;
        public string? outputFloder;

        public Dictionary<int, TransferQueue>? Transfers { get; set; } = [];

        public void Connect(string host, int port)
        {
            socket = new(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(host, port, ConnetionCallback, null);
        }

        private void ConnetionCallback(IAsyncResult ar)
        {
            string error = string.Empty;

            try
            {
                socket?.EndConnect(ar);
                if (socket?.RemoteEndPoint == null)
                    throw new Exception($"RemoteEndPoint is null");

                remoteEndPoint = (IPEndPoint)socket.RemoteEndPoint;
            }
            catch (Exception ex) { error = ex.Message; }
                

            connectCallBack?.Invoke(this, error);
        }

        public void BeginReceive()
        {
            try
            {
                if (buffer == null)
                    throw new Exception("buffer is null");

                socket?.BeginReceive(buffer, 0, buffer.Length, SocketFlags.Peek, ReceiveCallback, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (socket == null)
                    throw new Exception("client socket is null");

                int found = socket.EndReceive(ar);

                if(found > 4)
                {
                    socket.Receive(buffer ??= new byte[8192], 0, 4, SocketFlags.None);
                    int size = BitConverter.ToInt32(buffer, 0);
                    int read = socket.Receive(buffer, 0, size, SocketFlags.None);
                    while (read < size)
                    {
                        read += socket.Receive(buffer, read, size - read, SocketFlags.None);
                    }
                    Process();
                }
                Run();

            }
            catch (Exception ex)
            {
                Close();
                throw new Exception(ex.Message);
            }
        }

        public void Run()
        {
            try
            {
                socket?.BeginReceive(buffer, 0, buffer.Length, SocketFlags.Peek, ReceiveCallback, null);
            }
            catch (SocketException ex)
            {
                Close();
                throw new Exception(ex.Message);
            }
        }

        private void Process()
        {
            if (buffer == null) throw new Exception("buffer is null");

            using PacketReader pr = new(buffer);
            Headers header = (Headers)pr.ReadByte();

            switch (header)
            {
                case Headers.Queue:
                    {
                        int id = pr.ReadInt32();
                        string fileName = pr.ReadString();
                        long length = pr.ReadInt64();

                        TransferQueue queue = new();
                    }
                    break;
                case Headers.Start:
                    break;
                case Headers.Stop:
                    break;
                case Headers.Pause:
                    break;
                case Headers.Chunk:
                    break;
                default:
                    break;
            }
        }

        public void Send(byte[] data)
        {
            if (isClosed) return;
            lock (this)
            {
                try
                {
                    socket?.Send(BitConverter.GetBytes(data.Length), 0, 4, SocketFlags.None);
                    socket?.Send(data, 0, data.Length, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void ProgressStatus(TransferQueue queue)
        {
            ProgressChanged?.Invoke(this, queue);
        }

        public void Close()
        {
            isClosed = true;
            Transfers?.Clear();
            Transfers = null;
            buffer = new byte[8192];
            Disconnected?.Invoke(this, EventArgs.Empty);
            socket?.Close();
            socket = null;

        }

    }
}
