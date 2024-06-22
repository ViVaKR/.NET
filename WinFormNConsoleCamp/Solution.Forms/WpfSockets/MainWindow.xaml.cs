using System.IO;
using System.Windows;
using WpfSockets.Libs;
using WpfSockets.Models;

namespace WpfSockets
{
    public partial class MainWindow : Window
    {
        private readonly FileServerListener listener;
        private TransferClient? transferClient;
        private readonly string? outputFolder;
        private readonly System.Windows.Threading.DispatcherTimer tmrOverallProg;

        private readonly string serverIP;
        private readonly int serverPort;
        private bool running;

        private readonly List<TranferInfomation> infos;

        public MainWindow()
        {
            InitializeComponent();

            TbHost.Text = serverIP = "localhost";
            TbPort.Text = (serverPort = 60010).ToString();

            listener = new FileServerListener(SocketType.FileServer);
            tmrOverallProg = new() { Interval = new TimeSpan(0, 0, 1) };
            outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Download");
            running = false;

            infos =
            [
                new(1, "Hello", "Send", 10.5f),
                new(2, "Word", "Receive", 40.5f),
                new(3, "Fine", "Send", 15.5f),
                new(4, "Thanks", "Send", 10.5f),
            ];

            CreateEvents();
        }

        private void CreateEvents()
        {
            listener.SocketAccepted += Listener_SocketAccepted;
            tmrOverallProg.Tick += TmrOverallProg_Tick;
        }

        private void RemoveEvents()
        {
            if (transferClient == null) return;
            transferClient.Complete -= TransferClient_Complete;
            transferClient.Disconnected -= TransferClient_Disconnected;
            transferClient.ProgressChanged -= TransferClient_ProgressChanged;
            transferClient.Queued -= TransferClient_Queued;
            transferClient.Stopped -= TransferClient_Stopped;
        }
        private void RegisterEvents()
        {
            if (transferClient == null) return;
            transferClient.Complete += TransferClient_Complete;
            transferClient.Disconnected += TransferClient_Disconnected;
            transferClient.ProgressChanged += TransferClient_ProgressChanged;
            transferClient.Queued += TransferClient_Queued;
            transferClient.Stopped += TransferClient_Stopped;
        }

        private void TransferClient_Stopped(object sender, TransferQueue queue)
        {
            // TODO
        }

        private void TransferClient_Queued(object sender, TransferQueue queue)
        {
            // TODO
        }

        private void TransferClient_ProgressChanged(object sender, TransferQueue queue)
        {
            // TODO
        }

        private void TransferClient_Disconnected(object? sender, EventArgs e)
        {
            // TODO
        }

        private void TransferClient_Complete(object sender, TransferQueue queue)
        {
            // TODO
        }



        private void Listener_SocketAccepted(object sender, Libs.Args.SocketAcceptedEventArgs e)
        {

        }

        private void TmrOverallProg_Tick(object? sender, EventArgs e)
        {

        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            InfomationView.ItemsSource = infos;

            if (transferClient != null) return;

            transferClient = new TransferClient(ConnectingCallback);
            transferClient.Connect(serverIP, serverPort);

        }

        private void ConnectingCallback(object sender, string error)
        {
            // CheckAccess returns true if you're on the dispatcher thread
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new ConnectCallback(ConnectingCallback), sender, error);
                return;
            }
            if (!string.IsNullOrEmpty(error))
            {
                transferClient?.Close();
                transferClient = null;

                MessageBox.Show(error, "ERROR");
                return;
            }
            if (transferClient == null) return;
            RegisterEvents();
            transferClient.outputFloder = outputFolder;



        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (running) return;

            running = true;

            try
            {
                listener.Start();
                TbStatus.Text = "연결 대기중";
            }
            catch
            {
                running = false;
            }
        }
    }
}