using System;
using System.Globalization;
using System.Windows.Data;

namespace presentation {
    [ValueConversion(typeof(Date), typeof(DateTime?))]
    public class DateNullableDateTimeConverter : IValueConverter {
        public object Convert(object value, Type target_type, object parameter, CultureInfo culture) {
            return ((Date) value).ToDateTime();
        }

        public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture) {
            return ((DateTime?) value) ?? new Date();
        }
    }
}