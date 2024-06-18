using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfApp1
{
    [ValueConversion(typeof(double), typeof(double))]
    internal class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tf1 = double.TryParse(value.ToString(), out var actualSize);
            var tf2 = double.TryParse(parameter.ToString(), out var ratio);
            return (tf1 && tf2) ? actualSize * ratio : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value.ToString();
            if (double.TryParse(strValue, out double result))
            {
                return result;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
