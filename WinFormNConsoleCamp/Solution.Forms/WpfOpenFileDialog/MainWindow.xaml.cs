using Microsoft.Win32;
using System.Windows;


namespace WpfOpenFileDialog
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            TbInfo.FontFamily = new System.Windows.Media.FontFamily("FiraCode Nerd Font Mono");
            // TbInfo.Text = $"{(int)'✴':x}";
            TbInfo.Text = "\u2734";
        }

        private void BtnFire_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new();
            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                string path = fileDialog.FileName;
                string name = fileDialog.SafeFileName;
                TbInfo.Text = $"{path} : {name}";
            }
            else
            {
                // didnt pick anything
            }

        }
    }
}