using System;
using Microsoft.Maui.Controls;

namespace MmaFIghter.Services
{
    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return 0;

            // Adjust the multiplier based on your preference
            return System.Convert.ToDouble(value) * 15;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
