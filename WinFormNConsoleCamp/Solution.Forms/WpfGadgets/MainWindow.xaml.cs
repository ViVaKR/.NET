
using System.Windows;
using System.Windows.Threading;

namespace WpfGadgets
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Ellipse_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private DispatcherTimer timer;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetDateTime();
            timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void SetDateTime()
        {
            TbDate.Text = DateTime.Now.ToShortDateString();
            TbTime.Text = DateTime.Now.ToLongTimeString();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            SetDateTime();
        }
    }
}