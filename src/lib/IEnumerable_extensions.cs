using System.Collections.Generic;
using System.Linq;

namespace lib {
	public static class IEnumerable_extensions {
		public static IEnumerable<T> Clone<T>(this IEnumerable<T> enumerable) {
			return enumerable.Select(item => item);
		}
	}
}