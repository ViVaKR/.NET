using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfSockets
{
    public partial class MainWindow : Window
    {
        private string? displayedImage;

        public string? DisplayedImage
        {
            get => displayedImage;
            set => displayedImage = value;
        }

        public MainWindow()
        {
            DisplayedImage = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "circle_red.png");
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnFile_Click(object sender, RoutedEventArgs e)
        {
            // byte[] img = File.ReadAllBytes(DisplayedImage);
            // TbResult.Text = $"파일크기: {img.Length:N0} byte";

        }
    }
}