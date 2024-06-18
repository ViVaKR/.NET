
using System.Windows;

namespace WpfControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(TbValue.Text);
            TbValue.Text = ((number + 1).ToString());
        }
    }
}