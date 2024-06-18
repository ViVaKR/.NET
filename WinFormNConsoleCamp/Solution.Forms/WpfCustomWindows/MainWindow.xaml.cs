
using System.Windows;
using System.Windows.Input;


namespace WpfCustomWindows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // Close();
            Application.Current.Shutdown();
        }

        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            // 최대화 기능과 복원기능
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}