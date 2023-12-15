
using System.Windows;
using WpfMVVM.ViewModel;

namespace WpfMVVM
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel vm = new();
            DataContext = vm;
        }
    }
}