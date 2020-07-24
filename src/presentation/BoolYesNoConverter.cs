using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace presentation {
	[ValueConversion(typeof(bool), typeof(string))]
	public class BoolYesNoConverter : IValueConverter {
		public object Convert(object value, Type target_type, object parameter, CultureInfo culture) {
			return ((bool) value) ? "Yes" : "No";
		}

		public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture) {
			try {
				return (value as string).Substring(0, 1).ToLower() == "y";
			} catch (ArgumentOutOfRangeException) {
				return false;
			}
		}
	}
}