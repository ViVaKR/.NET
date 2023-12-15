
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
            ModalWindow modal = new(this)
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            Opacity = 0.4;
            modal.ShowDialog();
            Opacity = 1.0;
            if (modal.Success)
            {
                TbInput.Text = modal.Input ?? string.Empty;
            }
        }
    }
}