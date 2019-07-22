using System;
using System.Globalization;
using System.Windows.Data;

namespace AgpWps.Client.Converters
{
    /// <summary>
    /// Use this converter when you don't understand why the binding is bad or you're not sure what values is bound to an element.
    /// </summary>
    public class DebugConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
