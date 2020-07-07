using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace presentation {
	[ValueConversion(typeof(IEnumerable), typeof(string))]
	public class IEnumerableConverter : IValueConverter {
		public object Convert(object value, Type target_type, object parameter, CultureInfo culture) {
			IEnumerable enumerable = (IEnumerable) value;
			IEnumerator enumerator = enumerable.GetEnumerator();
			StringBuilder sb = new StringBuilder();
			if (enumerator.MoveNext()) {
				sb.Append(enumerator.Current.ToString());
			}
			while (enumerator.MoveNext()) {
				sb.Append(", ");
				sb.Append(enumerator.Current.ToString());
			}
			return sb.ToString();
		}

		public object ConvertBack(object value, Type target_type, object parameter, CultureInfo culture) {
			throw new InvalidOperationException();
		}
	}
}