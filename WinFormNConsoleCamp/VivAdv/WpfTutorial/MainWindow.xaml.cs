
using System.Windows;


namespace WpfTutorial
{
    public partial class MainWindow : Window
    {
        private readonly bool running;
        public MainWindow()
        {
            InitializeComponent();

            Width = 1400;
            Height = 1024;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            running = false;

            

        }

        //private void BtnRun_Click(object sender, RoutedEventArgs e)
        //{
        //    running = !running;
        //    (string, string) txbText = ("Running", "Stopped");
        //    (string, string) btnText = ("Run", "Stop");

        //    TxbStatus.Text =  running ? txbText.Item1 : txbText.Item2;
        //    BtnRun.Content = running ? btnText.Item2 : btnText.Item1;

        //}
    }
}

//MessageBox.Show("Test");
//WindowState = WindowState.Maximized;