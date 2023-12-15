
using System.Windows;
using WpfOpening.View;

namespace WpfOpening
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnNormal_Click(object sender, RoutedEventArgs e)
        {
            NormalWindow normal = new();
            normal.Show();
        }

        private void BtnModal_Click(object sender, RoutedEventArgs e)
        {
            ModalWindow modal = new()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            modal.ShowDialog();
            if (modal.Success)
            {
                TbInput.Text = modal.Input ?? string.Empty;
            }
        }
    }
}