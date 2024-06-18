
using System.Windows;

namespace WpfListView
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            LsvEntries.Items.Add("a");
            LsvEntries.Items.Add("b");
            LsvEntries.Items.Add("c");
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            LsvEntries.Items.Add(TbEntry.Text);
            TbEntry.Clear();
            TbEntry.Focus();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            // (by index)
            //int idx = LsvEntries.SelectedIndex;
            //LsvEntries.Items.RemoveAt(idx);

            // (by item)
            object item = LsvEntries.SelectedItem;

            var confirm = MessageBox.Show($"Are you sure you want to delete: `{item}`", "Confirm", MessageBoxButton.YesNo);
            if (confirm == MessageBoxResult.Yes)
                LsvEntries.Items.Remove(item);

        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            LsvEntries.Items.Clear();
        }
    }
}