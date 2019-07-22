using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AgpWps.Client.Converters
{
    /// <summary>
    /// Converts a value reference to visibility. If the value is null it will be collapsed, otherwise it will return visible.
    /// </summary>
    public class ValidReferenceToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
