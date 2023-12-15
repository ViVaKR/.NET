
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;


namespace WpfObsv
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> entries;

        public ObservableCollection<string> Entries
        {
            get { return entries; }
            set
            {
                entries = value;

            }
        }

        public MainWindow()
        {
            
            DataContext = this;
            
            InitializeComponent();
            entries = [];
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Entries.Add(TbEntry.Text);
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            string selectedItem = (string)LsvEntries.SelectedItem;
            Entries.Remove(selectedItem);
        }
    }
}