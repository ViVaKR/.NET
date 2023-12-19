
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace WpfOpening.View
{
    public partial class ModalWindow : Window
    {
        public bool Success { get; set; }
        public string? Input { get; set; }

        public ModalWindow(Window parentWindow)
        {
            Owner = parentWindow;

            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            Success = true;
            Input = TbInput.Text;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox textbox) return;

            BtnOK.IsEnabled = !string.IsNullOrEmpty(textbox.Text);;
        }
    }
}
