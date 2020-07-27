using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace presentation {
	[ValueConversion(typeof(Object), typeof(Visibility))]
	public class NullVisibilityConverter : IValueConverter {
		public object Convert(object value, Type target_type, object parameter, CultureInfo culture) {
			return value == null ? Visibility.Collapsed : Visibility.Visible;
		}

		public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture) {
			throw new InvalidOperationException();
		}
	}
}