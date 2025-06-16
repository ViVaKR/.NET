
using System.IO;

namespace WpfSockets.Libs
{
    public class TransferQueue
    {
        public readonly ManualResetEvent? pauseEvent;
        private const int fileBufferSize = 8175;
        public static readonly byte[] fileBuffer = new byte[fileBufferSize];
        public bool isRunning;
        public bool isPaused;

        public int Id { get; set; }
        public int? Progress { get; set; }
        public int? LastProgress { get; set; }
        public long? Transferred { get; set; }
        public string? FileName { get; set; }
        public TransferClient? Client { get; set; }
        public QueueType QueueType { get; set; }
        public Thread? Thrd { get; set; }
        public FileStream? Fs { get; set; }
        public long? Length { get; set; }
        public long Index { get; set; }

        public TransferQueue()
        {
            pauseEvent = new ManualResetEvent(true);
            isRunning = true;
        }

        public static TransferQueue? CreateUploadQueue(TransferClient client, string fileName)
        {
            try
            {
                var queue = new TransferQueue
                {
                    Id = Guid.NewGuid().GetHashCode(),
                    FileName = fileName,
                    Client = client,
                    QueueType = QueueType.Upload,
                    Fs = new FileStream(fileName, FileMode.Open),
                    Thrd = new Thread(TransferProc) { IsBackground = true }

                };
                queue.Length = queue.Fs.Length;

                return queue;
            }
            catch
            {
                return null;
            }
        }

        public static TransferQueue? CreateDownloadQueue(TransferClient client, int id, string fileName, long length)
        {
            try
            {
                TransferQueue queue = new()
                {
                    Id = id,
                    FileName = Path.GetFileName(fileName),
                    Client = client,
                    QueueType = QueueType.Download,
                    Fs = new FileStream(fileName, FileMode.Create),
                    Length = length

                };
                queue.Fs.SetLength(length);

                return queue;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Start()
        {
            isRunning = true;
            Thrd?.Start(this);
        }

        public void Write(byte[] bytes, long index)
        {
            if (Fs == null)
                throw new Exception("FileStram is null");

            lock (this)
            {
                Fs.Position = index;
                Fs.Write(bytes, 0, bytes.Length);
                Transferred += bytes.Length;
            }
        }

        public void Pause()
        {
            if (!isPaused)
                pauseEvent?.Reset();
            else
                pauseEvent?.Set();

            isPaused = !isPaused;
        }
        public void Stop()
        {
            isRunning = false;

        }

        public void Close()
        {
            if (Client == null) return;
            try
            {
                if (Client.Transfers != null && Client.Transfers.Any(x => x.Key == Id))
                {
                    Client?.Transfers?.Remove(Id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            isRunning = false;
            Fs?.Close();
            pauseEvent?.Dispose();
            Client?.Close();
            Client = null;

        }
        private static void TransferProc(object? obj)
        {
            try
            {
                if (obj == null)
                    throw new Exception("object is null");

                TransferQueue queue = (TransferQueue)obj
                    ?? throw new Exception("TransferQueue is null");

                if (queue.Fs == null)
                    throw new Exception("FileStream is null");

                while (queue.Index < queue.Length)
                {
                    if (!queue.isRunning) break;
                    queue.pauseEvent?.WaitOne();

                    int read = queue.Fs.Read(fileBuffer, 0, fileBuffer.Length);
                    PacketWriter pw = new();
                    pw.Write((byte)Headers.Chunk);
                    pw.Write(queue.Id); // 4 bytes
                    pw.Write(queue.Index); // 8 bytes
                    pw.Write(read);
                    pw.Write(fileBuffer, 0, read);

                    queue.Transferred += read;
                    queue.Index += read;

                    queue.Client?.Send(pw.GetBytes());

                    queue.Progress = Convert.ToInt32((queue.Transferred / queue.Length) * 100);

                    if (queue.LastProgress < queue.Progress)
                    {
                        queue.LastProgress = queue.Progress;
                        queue.Client?.ProgressStatus(queue);
                    }

                    Thread.Sleep(1);
                }

                queue?.Close();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
