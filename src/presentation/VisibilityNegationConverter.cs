using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace presentation {
	[ValueConversion(typeof(Visibility), typeof(Visibility))]
	public class VisibilityNegationConverter : IValueConverter {
		public object Convert(object value, Type target_type, object parameter, CultureInfo culture) {
			return ((Visibility) value) == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture) {
			return Convert(value, target_type, parameter, culture);
		}
	}
}