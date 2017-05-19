using System;
using System.Globalization;
using System.Windows.Data;

namespace Kandanda.Ui.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(DateTime))]
    public class TimeSpanToDateTimeConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = value as TimeSpan?;
            if (!timeSpan.HasValue)
                throw new ArgumentException($"{value} is not a TimeSpan");
            return DateTime.Today + timeSpan.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            if (!dateTime.HasValue)
                throw new ArgumentException($"{value} is not a DateTime");
            return dateTime.Value.TimeOfDay;
        }
    }
}
