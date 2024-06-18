using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                switch (WindowState)
                {
                    case WindowState.Maximized:
                        {
                            ResizeMode = ResizeMode.CanResize;
                            WindowStyle = WindowStyle.SingleBorderWindow;
                            WindowState = WindowState.Normal;
                        }
                        break;

                    case WindowState.Normal:
                        {
                            ResizeMode = ResizeMode.NoResize;
                            WindowStyle = WindowStyle.SingleBorderWindow;
                            WindowState = WindowState.Maximized;
                        }
                        break;
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BorderThickness = WindowState == WindowState.Maximized ? new Thickness(8) : new Thickness(0);
        }
    }
}
