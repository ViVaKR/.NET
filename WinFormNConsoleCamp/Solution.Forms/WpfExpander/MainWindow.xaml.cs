
using System.Windows;

namespace WpfExpander
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            ExpDetail.IsExpanded = !ExpDetail.IsExpanded;
        }
    }
}