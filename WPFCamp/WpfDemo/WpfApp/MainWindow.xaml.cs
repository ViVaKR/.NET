using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using WinForms = System.Windows.Forms;

namespace WpfApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string boundText = string.Empty;

        public string BoundText
        {
            get { return boundText; }
            set
            {
                boundText = value;
                
                OnPropertyChange();
            }
        }

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnSet_Click(object sender, RoutedEventArgs e)
        {
            BoundText = "Set From Code";
        }
        void OnPropertyChange([CallerMemberName]string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private void BtnRun_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new()
            {
                Filter = "C# Source Files | *.cs",
                InitialDirectory = Path.GetDirectoryName(path: Assembly.GetEntryAssembly()?.Location),
                Title = "Please pick a CS Source file(s)...",
                Multiselect = true
            };

            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                string[] fileNames = fileDialog.FileNames;
                tbFile.Text = string.Join(", ", fileNames);
            }

        }

        private void BtnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            // 윈폼 컨트롤 은 별칭을 사용하는 것을 권장
            WinForms.FolderBrowserDialog dialog = new()
            {
                InitialDirectory = Directory.GetCurrentDirectory() ?? Environment.CurrentDirectory
            };

            var result = dialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                tbFile.Text = dialog.SelectedPath;
            }
        }
    }
}
