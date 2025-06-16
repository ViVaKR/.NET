using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;

namespace WpfTutorial.View.UserControls
{
    public partial class ClearableTextBox : UserControl
    {
        [AllowNull]
        private string placeholder;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;

                // Do not do this!
                TxblPlaceholder.Text = placeholder;
                // OnPropertyChanged()
            }
        }

        public ClearableTextBox()
        {
            InitializeComponent();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxbInput.Clear();
            TxbInput.Focus();
        }

        private void TxbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox textbox) return;

            TxblPlaceholder.Visibility = string.IsNullOrEmpty(textbox.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
