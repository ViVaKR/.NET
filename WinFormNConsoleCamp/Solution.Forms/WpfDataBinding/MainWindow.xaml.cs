﻿using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WpfDataBinding
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [AllowNull]
        private string boundText;

        public string BoundText
        {
            get { return boundText; }
            set
            {
                boundText = value;
                OnPropertyChanged();

            }
        }

        public MainWindow()
        {
            // 점 3개 없에기
            DataContext = this;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void BtnSet_Click(object sender, RoutedEventArgs e)
        {
            BoundText = $"Set from code {(int)'✴'}";


        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BtnMessage_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Could not open file", "ERROR",MessageBoxButton.OK, MessageBoxImage.Error);

            MessageBoxResult result = MessageBox.Show("Do you agree?", "Agreement", MessageBoxButton.YesNo, MessageBoxImage.Question);
            TxblResult.Text = result == MessageBoxResult.Yes ? "Agreed" : "Not agreed";
        }
    }
}