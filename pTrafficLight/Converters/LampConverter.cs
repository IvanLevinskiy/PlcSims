using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace pSvetofor.Converters
{
    internal class LampConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (bool)value;

            if (state == true)
            {
                return Brushes.Green;
            }
            else
            {
                return Brushes.Red;
            }

            return Brushes.Gray;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
