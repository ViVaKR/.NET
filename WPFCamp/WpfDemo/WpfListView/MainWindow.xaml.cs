﻿using System;
using System.Collections.Generic;
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

namespace WpfListView
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                lvEntries.Items.Add($"{i:000}");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            lvEntries.Items.Add(txtEntry.Text);
        }
    }
}
