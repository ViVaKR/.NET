using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.View.UserControls
{
    public partial class ClearTextBox : UserControl
    {
        private string placeholder = string.Empty;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value;

                // do not do this
                tbPlaceholder.Text = placeholder;
                // OpPropertyChange()
            }
        }

        public ClearTextBox()
        {
            InitializeComponent();
        }


        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void TxtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not TextBox textblock) return;

            tbPlaceholder.Visibility =
                string.IsNullOrEmpty(textblock.Text)
                ? Visibility.Visible
                : Visibility.Hidden;
        }
    }
}
