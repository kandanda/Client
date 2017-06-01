using System;
using System.Globalization;
using System.Windows.Data;

namespace Kandanda.Ui.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(double))]
    public class TimeSpanToDoubleConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = value as TimeSpan?;
            if (!timeSpan.HasValue)
                throw new ArgumentException($"{value} is not a TimeSpan");
            return timeSpan.Value.TotalMinutes;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var minutes = value as double?;
            if (!minutes.HasValue)
                throw new ArgumentException($"{value} is not a double");
            return TimeSpan.FromMinutes(minutes.Value);
        }
    }
}
