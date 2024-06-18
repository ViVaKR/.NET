using System;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
{

    public partial class MainWindow : Window
    {
       

        readonly UserControl1 uc;

        public MainWindow()
        {
            InitializeComponent();

            uc = new UserControl1();
            BaseGrid.Children.Add(uc);

            uc.Txt_CardID.TextChanged += uc.LoginTextChanged;
            uc.Txt_Pw.TextChanged += uc.LoginTextChanged;
        }

        public void ControlManager(object? sender, EventArgs e)
        {
            foreach (UIElement element in BaseGrid.Children)
            {
                if (element is not Control ctrl) continue;
                ctrl.IsEnabled = true;
            }
        }
    }
}
