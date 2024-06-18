using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp.View.UserControls
{
    public partial class VivSlideBar : UserControl
    {
        public VivSlideBar()
        {
            InitializeComponent();

            VivSlider.IsTabStop = false;
            VivSlider.Focusable = false;
            VivSlider.OverridesDefaultStyle = true;
            VivSlider.Foreground = Brushes.Red;
            VivSlider.Minimum = 0;
            VivSlider.Maximum = 100;
            VivSlider.Value = 0;

            VivSlider.ValueChanged += VivSlider_ValueChanged;
        }

        private void VivSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ctbox.Content = Math.Round(e.NewValue);
        }

        private void BtnFire_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Could not open file.", "EROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            MessageBoxResult result =  MessageBox.Show("Do you agree?", "Agreement", MessageBoxButton.YesNo, MessageBoxImage.Question);
            tbInfo.Text = result == MessageBoxResult.Yes ? "감사합니다." : "다음에 다시 뵙겠습니다.";
        }
    }
}
