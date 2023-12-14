using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
